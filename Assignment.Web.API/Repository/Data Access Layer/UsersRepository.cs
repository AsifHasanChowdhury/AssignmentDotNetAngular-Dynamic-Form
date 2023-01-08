using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Assignment.Web.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment.Web.API.Repository.Data_Access_Layer
{
    public class UsersRepository : Interface.IUsersRepository
    {

        private readonly IConfiguration Configuration;//connection Interface

        public UsersRepository(IConfiguration config)
        {
            Configuration = config;//need it for Configuration in line 25
                                   //  _roleManager= roleManager;
        }

        public String loginAsync([FromBody] LoginModel user)
        {
            var tokenString = "";

            try
            {
                SqlConnection connection = new SqlConnection(Configuration
                .GetConnectionString("DefaultConnection").ToString());

                connection.Open();



                StringBuilder loadInforamtion =
                new StringBuilder
                ("SELECT Oid,userRole FROM dbo.userInfo WHERE userEmail= 'email' AND userPassword= 'password' ");

                loadInforamtion.Replace("email", user.UserName);
                loadInforamtion.Replace("password", user.Password);

                SqlCommand comm = new SqlCommand(loadInforamtion.ToString(), connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                String RoleName = "NoRole";
                int UserID = 0;

                if (dt.Rows.Count > 0)
                {
                    RoleName = Convert.ToString(dt.Rows[0]["userRole"]);
                    UserID = Convert.ToInt32(dt.Rows[0]["Oid"]);
                }


                if (UserID > 0 && RoleName != "NoRole")
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, RoleName),
                        new Claim(ClaimTypes.GroupSid,UserID.ToString())
                    };

                    var tokeOptions = new JwtSecurityToken(
                        issuer: "https://localhost:44379",
                        audience: "https://localhost:44379",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );

                    tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return tokenString;

        }
    }
}
