using Newtonsoft.Json.Linq;

namespace Assignment.Web.API.Repository.Interface
{
    public interface IFormJsonRepository
    {
        public string FormRepository(int id);

        public void storeFormData(Object json, string form, string formTable);

        public String FetchApplicationList();

        public void updateFormInformation(Object json, string form, string formTable);

    }
}
