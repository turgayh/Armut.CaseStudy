﻿using Armut.CaseStudy.Model;
using Armut.CaseStudy.Operation.Helper;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Armut.CaseStudy.Operation.UserServices
{
    public class UserService : IUserService
    {

        private readonly string TokenExpiry;
        private readonly string Audience;
        private readonly string Issuer;
        private readonly string SecretKey;
        private readonly IMongoCollection<SingupModel> userContext;
        private readonly IMongoCollection<User> userInfoContext;

        public UserService(IJwtToken jwtToken, IDatabaseSettings settings)
        {
            TokenExpiry = jwtToken.TokenExpiry;
            Audience = jwtToken.Audience;
            Issuer = jwtToken.Issuer;
            SecretKey = jwtToken.SecretKey;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            userContext = database.GetCollection<SingupModel>(settings.UsersCollectionName);
            userInfoContext = database.GetCollection<User>(settings.UserInfoCollectionName);

        }

        public string BuildJWTToken()
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

        public bool Authenticate(LoginModel login)
        {
            bool validUser = false;

            SingupModel user = userContext.Find(user => user.Username == login.Username).FirstOrDefault();
            if (user == null) return false;
            bool validPass = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
            if (login.Username == user.Username && validPass)
            {
                validUser = true;
            }
            return validUser;
        }




        public ServiceResponse<User> CreateUser(string id, string username)
        {
            User user = new User();
            ServiceResponse<User> response = new ServiceResponse<User>();
            user.UserId = id;
            user.Username = username;
            response.Data = user;
            userInfoContext.InsertOne(user);
            return response;
        }

        public ServiceResponse<User> Signup(SingupModel singup)
        {
            //   bool verified = BCrypt.Net.BCrypt.Verify("Pa$$w0rd", passwordHash);
            ServiceResponse<User> response = new ServiceResponse<User>();
            singup.Password = BCrypt.Net.BCrypt.HashPassword(singup.Password);
            userContext.InsertOne(singup);
            SingupModel user = userContext.Find<SingupModel>(user => user.Username == singup.Username).FirstOrDefault();
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
    }
}
