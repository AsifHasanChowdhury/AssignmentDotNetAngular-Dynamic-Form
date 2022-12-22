using Microsoft.AspNetCore.Mvc;

namespace Assignment.Web.API.Repository.Interface
{
    public interface IUsersRepository
    {
        public string loginAsync([FromBody] Object json);
    }
}
