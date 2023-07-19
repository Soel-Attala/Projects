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

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {

                var Token = new UserToken();

                var searchUser = (from user in _context.Users
                                  where user.Name == userLogins.UserName && user.Password == userLogins.Password
                                  select user).FirstOrDefault();

                Console.WriteLine("User Found: ", searchUser);


                //var Valid = Logins.Any(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));

                if (searchUser != null)
                {
                    //var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenKey(new UserToken()
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
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
            return Ok(_context.Users);
        }
    }
}
