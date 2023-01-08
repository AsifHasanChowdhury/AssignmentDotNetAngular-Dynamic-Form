using Assignment.Web.API.Repository.Data_Access_Layer;
using Assignment.Web.API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Security.Claims;

namespace Assignment.Web.API.Controllers
{
    [Authorize(Roles = "AdminUser,GeneralUser")]
    [Route("[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private IFormJsonRepository FormJsonRepository;

        public FormController(IConfiguration configuration)
        {
            FormJsonRepository = new FormJsonRepository(configuration);
            // _httpClient.BaseAddress = baseAddress;
        }




        [HttpPost]
        [Route("SendFormModule")]
        public string SendFormAngularApp([FromBody] Object json)
        {
            var FormJsonFormat = "";
            try
            {

                var data = JsonConvert.DeserializeObject<dynamic>(json.ToString());

                int formID = 0;
                foreach (JProperty property in data.Properties())
                {
                    formID = (int)property.Value;
                    break;
                }

                FormJsonFormat = JsonConvert
                    .SerializeObject(FormJsonRepository.FormRepository(formID));
            }

            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
            return FormJsonFormat;
        }





        [HttpPost]
        [Route("GetFormResponse")]
        public void RecieveFormResponse([FromBody] Object json)
        {
            try
            {

                //FormJsonRepository.storeFormData(json);

                var data = JsonConvert.DeserializeObject<dynamic>(json.ToString());
                //var keyValuePairs = JObject.Parse(data.ToString());
                //var value = keyValuePairs["email"];

                string email = "";
                string FormTable = "";

                foreach (JProperty property in data.Properties())
                {
                    // Debug.WriteLine(property.Name + " ---- " + property.Value);
                    if (property.Name == "email")
                    {
                        email = (string)property.Value;
                    }
                    else if (property.Name == "formType") //which table will get the data
                    {
                        FormTable = (string)property.Value;
                    }

                }

                FormJsonRepository.storeFormData(json, email, FormTable);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }


        [HttpPost]
        [Route("ShowAllRequests")]
        public IActionResult ApplicationList()
        {


            String FormList = "";

            try
            {
                ClaimsPrincipal currentUser = this.User;

                if (currentUser.IsInRole("AdminUser"))
                {

                    FormList = FormJsonRepository.FetchApplicationList();
                }
                else
                {
                    //  return RedirectToAction(SearchbyEmail(currentUser.Claims.ToList()));
                    FormList = SearchbyEmail(currentUser.Claims.ToList());
                }
                //var FormJsonFormat = JsonConvert
                //    .SerializeObject(FormJsonRepository.FetchApplicationList());

                // return FormJsonFormat;

            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return  Ok(FormList);
        }


        [HttpPost]
        [Route("UpdateFormInfo")]

        public void UpdateInformationbyAdmin([FromBody] Object json)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<dynamic>(json.ToString());
                int Oid = data["Oid"];
                string FormTable = data["formType"];
                FormJsonRepository.updateFormInformation(data, Oid, FormTable);
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

        }



        [HttpPost]
        [Route("DeleteRequest")]

        public void DeleteFormResponsebyAdmin([FromBody] Object json)
        {

            try
            {
                var data = JsonConvert.DeserializeObject<dynamic>(json.ToString());
                int Oid = data["Oid"];
                string FormTable = data["formType"];
                FormJsonRepository.deleteFormInformation(Oid, FormTable);

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        [HttpPost]
        [Route("SearchApplicationbyEmail")]

        public string SearchbyEmail(List<Claim> userClaim)
        {

            // var userID = JsonConvert.DeserializeObject<dynamic>(json.ToString());
            //userID = Convert.ToInt32(userID["userID"]);
            var ApplicationListResponse = "";

            try
            {
                foreach (var item in userClaim)
                {
                    if (item.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid"))
                    {

                        // Debug.WriteLine(item.Value);
                        ApplicationListResponse = FormJsonRepository
                                               .FetchApplicationbyEmail
                                               (Convert.ToInt32(item.Value));

                        return ApplicationListResponse;

                    }

                }
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

          

            return ApplicationListResponse;


        }

    }
}
