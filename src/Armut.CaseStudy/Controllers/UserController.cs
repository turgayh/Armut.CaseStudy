using System;
using Armut.CaseStudy.Model;
using Armut.CaseStudy.Operation.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Armut.CaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            _logger.LogInformation("UserController-Login Index executed at {date}", DateTime.UtcNow);
            if (login == null) return Unauthorized();
            var response = _userService.Login(login);
            if (!response.Success)
                return Unauthorized();
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult Signup([FromBody] SingupModel signup)
        {
            _logger.LogInformation("UserController-Signup Index executed at {date}", DateTime.UtcNow);

            if (signup == null) return NotFound();
            ServiceResponse<string> userCheck = _userService.CheckUsername(signup.Username);
            if (userCheck.Success) return Ok(userCheck);
            return Ok(_userService.Signup(signup));
        }



    }
}
