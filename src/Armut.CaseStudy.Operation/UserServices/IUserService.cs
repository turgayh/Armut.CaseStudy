using Armut.CaseStudy.Model;


namespace Armut.CaseStudy.Operation.UserServices
{
    public interface IUserService
    {
        public ServiceResponse<User> CreateUser(string id, string username);
        public bool Authenticate(LoginModel login);
        public ServiceResponse<User> Singup(SingupModel singup);
        public string BuildJWTToken();

    }
}
