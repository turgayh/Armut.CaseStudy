using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Armut.CaseStudy.Model;
using Armut.CaseStudy.Operation.Helper;
using Armut.CaseStudy.Operation.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            string tokenString = string.Empty;
            bool validUser = userService.Authenticate(login);
            if (validUser)
            {
                tokenString = userService.BuildJWTToken();
            }
            else
            {
                return Unauthorized();
            }
            return Ok(new { Token = tokenString });
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult Singup([FromBody] SingupModel singup)
        {
            if (singup == null) return NotFound();

            return Ok(userService.Singup(singup));
        }



    }
}
