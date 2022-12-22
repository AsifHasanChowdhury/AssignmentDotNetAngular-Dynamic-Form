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

        
        [HttpPost]
        [Route("UserLogin")]
        public string Login([FromBody] Object json)
        {
            /*
                var data = JsonConvert
                          .DeserializeObject<dynamic>(json.ToString());
            */
               // var userRole = _UsersRepository.loginAsync(json);
                var userRole = "Admin";

                if ( userRole != "NoRole")
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, "Shimanto@gmail.com"),
                        new Claim(ClaimTypes.Name,"Asif Hasan"),
                        new Claim(ClaimTypes.Role,userRole),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(5000000),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        //IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTime.UtcNow,
                        // The time at which the authentication ticket was issued.

                        RedirectUri = "https://www.facebook.com/"
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };



                    HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);


                return userRole;
            }

                return userRole;

            
        }
        
    }
}
