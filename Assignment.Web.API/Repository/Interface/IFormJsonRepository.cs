using Newtonsoft.Json.Linq;

namespace Assignment.Web.API.Repository.Interface
{
    public interface IFormJsonRepository
    {
        public string FormRepository(int id);

        public void storeFormData(Object json, string form, string formTable);

        public String FetchApplicationList();

        public void updateFormInformation(Object json, int Oid, string formTable);

        public void deleteFormInformation(int Oid, string formTable);

        public String FetchApplicationbyEmail(int id);


    }
}
