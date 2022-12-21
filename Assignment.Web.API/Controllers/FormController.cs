using Assignment.Web.API.Repository.Data_Access_Layer;
using Assignment.Web.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Dynamic;

namespace Assignment.Web.API.Controllers
{

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

            var data = JsonConvert.DeserializeObject<dynamic>(json.ToString());

            int formID = 0;
            foreach (JProperty property in data.Properties())
            {
                formID= (int)property.Value;
                break;
            }

            var FormJsonFormat = JsonConvert
                .SerializeObject(FormJsonRepository.FormRepository(formID));

            return FormJsonFormat;
        }



        [HttpPost]
        [Route("GetFormResponse")]
        public void RecieveFormResponse([FromBody] Object json)
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
                    if(property.Name == "email")
                    {
                        email = (string)property.Value;
                    }   
                    else if(property.Name == "formType") //which table will get the data
                    {
                        FormTable = (string)property.Value;
                    }

            }

            FormJsonRepository.storeFormData(json,email,FormTable);

        }


        [HttpPost]
        [Route("ShowAllRequests")]
        public String ApplicationList()
        {
            //var FormJsonFormat = JsonConvert
            //    .SerializeObject(FormJsonRepository.FetchApplicationList());

            // return FormJsonFormat;
            String FormList = FormJsonRepository.FetchApplicationList();
          
            return FormList;
        }

    }
}
