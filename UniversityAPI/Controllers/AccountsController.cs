using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.DataAcces;
using UniversityAPI.Helpers;
using UniversityAPI.Models.DataModels;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UniversityContext _context;
        private readonly JwtSettings _jwtSettings;

        public AccountsController(UniversityContext context, JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
            _context = context;
        }

        //TODO: Change by real users in DB
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Name = "Admin",
                Email = "testuser@gmail.com",
                Password = "Admin",
            },


        new User()
        {
            Id = 2,
                Name = "User1",
                Email = "alfajorjorgito@gmail.com",
                Password = "alfajorJorgito",
            }
    };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {

                var Token = new UserToken();
                var Valid = Logins.Any(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));

                if (Valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenKey(new UserToken()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuId = Guid.NewGuid(),
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong user or pasword");
                }
                return Ok(Token);

            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public ActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }
}
