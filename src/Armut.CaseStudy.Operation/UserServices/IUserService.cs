using Armut.CaseStudy.Model;


namespace Armut.CaseStudy.Operation.UserServices
{
    public interface IUserService
    {
        public ServiceResponse<User> CreateUser(string id, string username);
        public bool Authenticate(LoginModel login);
        public ServiceResponse<string> Login(LoginModel login);
        public ServiceResponse<User> Signup(SingupModel singup);
        public ServiceResponse<string> GetUserIdByUsername(string username);
        public ServiceResponse<string> CheckUsername(string username);
        public ServiceResponse<string> UserAddToBlockedList(string username);

    }
}
