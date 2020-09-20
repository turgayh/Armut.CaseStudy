using Armut.CaseStudy.Model;
using Armut.CaseStudy.Operation.Helper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Armut.CaseStudy.Operation.UserServices
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IContext _context;
        private IJwtToken _jwtToken;
        //public IDatabaseSettings Settings { get; }

        public UserService(IJwtToken jwtToken, ILogger<UserService> logger, IContext context)
        {
            _jwtToken = jwtToken;
            _logger = logger;
            _context = context;
        }

        private string BuildJWTToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtToken.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = _jwtToken.Issuer;
            var audience = _jwtToken.Audience;
            var jwtValidity = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtToken.TokenExpiry));

            var token = new JwtSecurityToken(issuer,
              audience,
              expires: jwtValidity,
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool Authenticate(LoginModel login)
        {
            bool validUser = false;
            var userContext = _context.UserContext();
            SingupModel user = userContext.Find(user => user.Username == login.Username).FirstOrDefault();
            if (user == null)
            {
                _logger.LogInformation("Userservice-Authenticate invalid username");
                return false;
            };
            bool validPass = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
            if (login.Username == user.Username && validPass)
            {
                validUser = true;
            }
            else
            {
                _logger.LogCritical("Userservice-Authenticate invalid password");
            }
            return validUser;
        }




        public ServiceResponse<User> CreateUser(string id, string username)
        {
            _logger.LogInformation("Userservice-CreateUser create new user");
            User user = new User();
            var userInfoContext = _context.UserInfoContext();
            ServiceResponse<User> response = new ServiceResponse<User>();
            try
            {
                user.UserId = id;
                user.Username = username;
                response.Data = user;
                userInfoContext.InsertOne(user);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Userservice-CreateUser error");
                throw;
            }
            return response;
        }

        public ServiceResponse<User> Signup(SingupModel singup)
        {

            _logger.LogInformation("Userservice-Signup registir user");
            SingupModel user;
            var userContext = _context.UserContext();
            try
            {
                ServiceResponse<User> response = new ServiceResponse<User>();
                singup.Password = BCrypt.Net.BCrypt.HashPassword(singup.Password);
                userContext.InsertOne(singup);
                user = userContext.Find<SingupModel>(user => user.Username == singup.Username).FirstOrDefault();
            }
            catch (Exception err)
            {
                _logger.LogError(err, "Userservice-Signup error");
                throw;
            }
            return CreateUser(user.UserId, user.Username);
        }


        public ServiceResponse<string> Login(LoginModel login)
        {
            string tokenString = string.Empty;
            bool validUser = Authenticate(login);
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (validUser)
            {
                tokenString = BuildJWTToken();
            }
            else
            {
                response.Success = false;
                response.Data = "INVALID USER";
                return response;
            }
            response.Data = tokenString;
            return response;
        }

        public ServiceResponse<string> GetUserIdByUsername(string username)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            SingupModel user = new SingupModel();
            var userContext = _context.UserContext();

            try
            {
                user = userContext.Find(user => user.Username == username).FirstOrDefault();
                if (user == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "INVALID USERNAME";
                    return response;
                }
                response.Data = user.UserId;

            }
            catch (Exception err)
            {
                _logger.LogError(err, "Userservice-GetUserIdByUsername throw error");
                throw;
            }
            return response;
        }

        public ServiceResponse<string> CheckUsername(string username)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            SingupModel user = new SingupModel();
            var userContext = _context.UserContext();

            try
            {
                user = userContext.Find(user => user.Username == username).FirstOrDefault();
                if (user != null)
                {
                    response.Success = true;
                    response.Data = "Username is already exits";
                    return response;
                }
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message, "Userservice-CheckUsername throw error");
                throw;
            }
            response.Success = false;
            response.ErrorMessage = "Username is not found";
            return response;
        }

    }
}
