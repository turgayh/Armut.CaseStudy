using Armut.CaseStudy.Model;
using Armut.CaseStudy.Operation.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Armut.CaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login == null) return Unauthorized();
            var response = userService.Login(login);
            if (!response.Success)
                return Unauthorized();
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult Singup([FromBody] SingupModel singup)
        {
            if (singup == null) return NotFound();

            return Ok(userService.Signup(singup));
        }



    }
}
