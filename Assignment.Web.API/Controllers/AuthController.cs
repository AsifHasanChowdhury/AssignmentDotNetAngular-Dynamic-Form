using Assignment.Web.API.Repository.Data_Access_Layer;
using Assignment.Web.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Newtonsoft.Json;
using Assignment.Web.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Assignment.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUsersRepository _UsersRepository;


        public AuthController(IConfiguration configuration)
        {
            this._UsersRepository = new UsersRepository(configuration);
            // _httpClient.BaseAddress = baseAddress;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        
        public IActionResult Login([FromBody] LoginModel user)
        {
            var JWTtoken="";

            if (user is not null)
            {
                 JWTtoken = _UsersRepository.loginAsync(user);

                if (JWTtoken.Length > 25)
                {
                    return Ok(new AuthenticatedResponse { Token = JWTtoken });
                }

            }

            else if(user is null)
            {
                return BadRequest("Invalid client request");
            }

            return Unauthorized();
        }


        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser([FromBody] Object json)
        {
            return null;
        }

    }
}
