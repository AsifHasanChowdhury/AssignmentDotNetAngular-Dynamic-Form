using Assignment.Web.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;

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

                SqlConnection connection = new SqlConnection
                    (Configuration.GetConnectionString("DefaultConnection").ToString());

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

                //this query checking whether the user email exists or not before inserting it to db


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

                //string loadInforamtion = "SELECT waterTable.Oid AS WTOID, waterTable.FormResponse AS WFR, " +
                //    "HomePermitTable.Oid AS HTOID, HomePermitTable.FormResponse AS HPFR," +
                //    "birthTable.Oid  AS BTOID , birthTable.FormResponse AS BFR " +
                //    "FROM userInfo " +
                //    "RIGHT JOIN waterTable ON waterTable.UserOid = userInfo.Oid " +
                //    "LEFT JOIN HomePermitTable ON HomePermitTable.UserId = userInfo.Oid " +
                //    "LEFT JOIN birthTable ON birthTable.UserOid = userInfo.Oid";

                string loadInformation = 
               "SELECT Oid AS TableId , FormResponse FROM dbo.waterTable UNION ALL\r\nSELECT Oid As TableID , FormResponse FROM dbo.birthTable UNION ALL\r\nSELECT Oid As TableID , FormResponse FROM dbo.HomePermitTable";


                SqlCommand comm = new SqlCommand(loadInformation, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);

                JsonList.Append('[') ;

                for(int i=0; i<dt.Rows.Count; i++)
                {
                    /*
                    if (dt.Rows[i]["WFR"].ToString() != "")
                    {
                       
                        JObject WFRjson = JObject.Parse(dt.Rows[i]["WFR"].ToString());

                        if (!WFRjson.ContainsKey("Oid"))
                        {
                            WFRjson.Add("Oid", dt.Rows[i]["WTOID"].ToString());

                        }

                        JsonList.Append(Convert.ToString(WFRjson));
                        JsonList.Append(',');

                    }
                    if (dt.Rows[i]["HPFR"].ToString() != "")
                    {
                        JObject HPFRjson = JObject.Parse(dt.Rows[i]["HPFR"].ToString());

                        if (!HPFRjson.ContainsKey("Oid"))
                        {
                            HPFRjson.Add("Oid", dt.Rows[i]["HTOID"].ToString());

                        }



                        JsonList.Append(Convert.ToString(HPFRjson));
                        JsonList.Append(',');

                    }
                    if (dt.Rows[i]["BFR"].ToString() != "")
                    {
                        JObject BFRjson = JObject.Parse(dt.Rows[i]["BFR"].ToString());

                        if (!BFRjson.ContainsKey("Oid"))
                        {
                            BFRjson.Add("Oid", dt.Rows[i]["BTOID"].ToString());

                        }

                        JsonList.Append(Convert.ToString(BFRjson));
                        JsonList.Append(',');

                    }
                    */
                    if (dt.Rows[i]["FormResponse"].ToString() != "")
                    {

                        JObject FRjson = JObject.Parse(dt.Rows[i]["FormResponse"].ToString());

                        if (!FRjson.ContainsKey("Oid"))
                        {
                            FRjson.Add("Oid", dt.Rows[i]["TableId"].ToString());

                        }

                        JsonList.Append(Convert.ToString(FRjson));
                        JsonList.Append(',');

                    }
                }
                //JsonList.Remove(JsonList.Length - 2,JsonList.Length-1);
                JsonList.Length--;
                JsonList.Append("]");
                
                


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

           return JsonList.ToString();
        }


        public void updateFormInformation([FromBody] Object json, int TableID, string formTable)
        {
            try
            {

                SqlConnection connection =
                    new SqlConnection(Configuration
                    .GetConnectionString("DefaultConnection")
                    .ToString());


                connection.Open();

                StringBuilder queryString =
                new StringBuilder
                ("UPDATE [UserInfoTable] SET FormResponse= 'updatedData' WHERE Oid = Tableid;");

                queryString.Replace("[UserInfoTable]", formTable);

                queryString.Replace("updatedData", json.ToString());

                queryString.Replace("Tableid", TableID.ToString());  
                //queryString.Replace("@userInformation", json);

                SqlCommand cmd = new SqlCommand(queryString.ToString(), connection);

                //cmd.Parameters.AddWithValue("@id", 2);
                //cmd.Parameters.AddWithValue("@userInformation", json.ToString());

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
             {
                Console.WriteLine(e.Message);
             }

        }


        public void deleteFormInformation(int Oid, String FormTable)
        {
            try
            {

                SqlConnection connection =
                    new SqlConnection(Configuration
                    .GetConnectionString("DefaultConnection")
                    .ToString());


                connection.Open();

                StringBuilder queryString =
                new StringBuilder("DELETE FROM [UserInfoTable] WHERE Oid = TableID");

                queryString.Replace("[UserInfoTable]", FormTable);

                queryString.Replace("TableID", Oid.ToString());

                SqlCommand cmd = new SqlCommand(queryString.ToString(), connection);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public string FetchApplicationbyEmail()
        {
            StringBuilder JsonList = new StringBuilder();

            try
            {

                SqlConnection connection = new SqlConnection(Configuration
                .GetConnectionString("DefaultConnection")
                .ToString());

                connection.Open();

                StringBuilder loadInformation =
                new StringBuilder("SELECT Oid AS TableId , FormResponse FROM dbo.waterTable UNION ALL\r\nSELECT Oid As TableID , FormResponse FROM dbo.birthTable UNION ALL\r\nSELECT Oid As TableID , FormResponse FROM dbo.HomePermitTable");

                SqlCommand comm = new SqlCommand(loadInformation.ToString(), connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);

                JsonList.Append('[');

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                  
                    if (dt.Rows[i]["FormResponse"].ToString() != "")
                    {

                        JObject FRjson = JObject.Parse(dt.Rows[i]["FormResponse"].ToString());

                        if (!FRjson.ContainsKey("Oid"))
                        {
                            FRjson.Add("Oid", dt.Rows[i]["TableId"].ToString());

                        }

                        JsonList.Append(Convert.ToString(FRjson));
                        JsonList.Append(',');

                    }
                }
                //JsonList.Remove(JsonList.Length - 2,JsonList.Length-1);
                JsonList.Length--;
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
