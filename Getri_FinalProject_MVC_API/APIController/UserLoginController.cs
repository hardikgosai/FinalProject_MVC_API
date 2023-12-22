using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;

namespace Getri_FinalProject_MVC_API.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly ILoginRepository loginRepository;

        public UserLoginController(ILoginRepository _loginRepository)
        {
            loginRepository = _loginRepository;
        }

        [HttpGet("GetUserLogin")]
        public IActionResult GetUser(string email, string password)
        {
            var user = loginRepository.AuthenticateUser(email, password);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
