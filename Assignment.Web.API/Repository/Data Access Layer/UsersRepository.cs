using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

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

        public String loginAsync([FromBody] Object json)
        {


            SqlConnection connection = new SqlConnection(Configuration
            .GetConnectionString("DefaultConnection").ToString());

            connection.Open();

            /*
                        //Give Role For User Email
                        string loadInforamtion = "SELECT RoleName FROM RoleTable WHERE RoleID IN (SELECT RoleID FROM UserMapRole_Table " +
                        "INNER JOIN User_Table ON (SELECT id FROM User_Table WHERE useremail='" + pl.email + "')=UserMapRole_Table.UserID)";
            */
            string loadInforamtion = "";
            SqlCommand comm = new SqlCommand(loadInforamtion, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            String RoleName = "NoRole";

            if (dt.Rows.Count > 0)
            {
                RoleName = Convert.ToString(dt.Rows[0]["RoleName"]);
            }

            return RoleName;

        }
    }
}
