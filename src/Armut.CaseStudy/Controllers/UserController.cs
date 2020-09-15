using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Armut.CaseStudy.Model;
using Armut.CaseStudy.Operation.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Armut.CaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UserController : ControllerBase
    {
        private readonly string TokenExpiry;
        private readonly string Audience;
        private readonly string Issuer;
        private readonly string SecretKey;

        public UserController(IJwtToken jwtToken)
        {
             TokenExpiry = jwtToken.TokenExpiry;
             Audience = jwtToken.Audience;
             Issuer = jwtToken.Issuer;
             SecretKey = jwtToken.SecretKey;
        }

        private string BuildJWTToken()
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = Issuer;
            var audience = Audience;
            var jwtValidity = DateTime.Now.AddMinutes(Convert.ToDouble(TokenExpiry));

            var token = new JwtSecurityToken(issuer,
              audience,
              expires: jwtValidity,
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody] LoginModel login)
        {
            if (login == null) return Unauthorized();
            string tokenString = string.Empty;
            bool validUser = Authenticate(login);
            if (validUser)
            {
                tokenString = BuildJWTToken();
            }
            else
            {
                return Unauthorized();
            }
            return Ok(new { Token = tokenString });
        }

        private bool Authenticate(LoginModel login)
        {
            bool validUser = false;

            if (login.Username == "hakan" && login.Password == "1234")
            {
                validUser = true;
            }
            return validUser;
        }

        [Authorize]
        [HttpGet("users")]
        public string Get()
        {
            return "selam yisen!!1";
        }
    }
}
