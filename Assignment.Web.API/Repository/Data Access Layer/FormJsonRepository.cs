using Assignment.Web.API.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Assignment.Web.API.Repository.Data_Access_Layer
{
    public class FormJsonRepository:IFormJsonRepository
    {

        private readonly IConfiguration Configuration;//connection Interface

        public FormJsonRepository(IConfiguration config)
        {
            Configuration = config;
        }

        public string FormRepository(int id)
        {
            string Form = "";
            try
            {

                SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection").ToString());

                connection.Open();
                string loadInforamtion = "SELECT JsonForm FROM formTable";
                SqlCommand comm = new SqlCommand(loadInforamtion, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    Form = Convert.ToString(dt.Rows[id]["JsonForm"]);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Form;

        }


        public void storeFormData(Object json,string email,string formTable)
        {
            try
            {

                SqlConnection connection =
                    new SqlConnection(Configuration
                    .GetConnectionString("DefaultConnection")
                    .ToString());


                connection.Open();

                StringBuilder userEmailQuery= 
                new StringBuilder
                ("INSERT INTO dbo.userInfo(userEmail) SELECT 'email' WHERE NOT EXISTS " +
                "(SELECT userEmail FROM dbo.userInfo  WHERE userEmail = 'checkEmail')");

                userEmailQuery.Replace("email", email);
                userEmailQuery.Replace("checkEmail", email);

                SqlCommand cmd = new SqlCommand (userEmailQuery.ToString(), connection);

                //cmd.Parameters.AddWithValue("@userEmail", email);

                cmd.ExecuteNonQuery();


                StringBuilder queryString=
                new StringBuilder
                ("INSERT INTO [UserInfoTable] VALUES ((SELECT Oid FROM userInfo WHERE userEmail= 'email' ), @userInformation);");

                queryString.Replace("[UserInfoTable]", formTable);

                queryString.Replace("email",email);
                //queryString.Replace("@userInformation", json);

                cmd = new SqlCommand (queryString.ToString(), connection);

                //cmd.Parameters.AddWithValue("@id", 2);
                cmd.Parameters.AddWithValue("@userInformation", json.ToString());

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        
        public string FetchApplicationList()
        {
            StringBuilder JsonList = new StringBuilder();

            try
            {

                SqlConnection connection = new SqlConnection(Configuration
                .GetConnectionString("DefaultConnection")
                .ToString());

                connection.Open();

                string loadInforamtion = "SELECT waterTable.FormResponse AS WFR, " +
                    "HomePermitTable.FormResponse AS HPFR," +
                    "birthTable.FormResponse AS BFR " +
                    "FROM userInfo " +
                    "RIGHT JOIN waterTable ON waterTable.UserOid = userInfo.Oid " +
                    "LEFT JOIN HomePermitTable ON HomePermitTable.UserId = userInfo.Oid " +
                    "LEFT JOIN birthTable ON birthTable.UserOid = userInfo.Oid";

                SqlCommand comm = new SqlCommand(loadInforamtion, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);

                JsonList.Append('[') ;

                for(int i=0; i<dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["WFR"].ToString() != "")
                    {
                        JsonList.Append(Convert.ToString(dt.Rows[i]["WFR"]));
                        if(i != dt.Rows.Count - 1) JsonList.Append(",");

                    }
                    if (dt.Rows[i]["HPFR"].ToString() != "")
                    {
                        JsonList.Append(Convert.ToString(dt.Rows[i]["HPFR"]));

                        if (i != dt.Rows.Count - 1) JsonList.Append(",");

                    }
                    if (dt.Rows[i]["BFR"].ToString() != "")
                    {
                        JsonList.Append(Convert.ToString(dt.Rows[i]["BFR"]));

                        if (i != dt.Rows.Count - 1) JsonList.Append(",");

                    }

                }
                JsonList.Append("]");


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

           return JsonList.ToString();
        }
        
    }
}
