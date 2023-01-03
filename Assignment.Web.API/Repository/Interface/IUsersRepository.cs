using Assignment.Web.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Web.API.Repository.Interface
{
    public interface IUsersRepository
    {
        public String loginAsync([FromBody] LoginModel user);
    }
}
