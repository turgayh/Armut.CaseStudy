

namespace Armut.CaseStudy.Operation.Helper
{
    public class JwtToken : IJwtToken
    {
        public string SecretKey { get; set ; }
        public string Issuer { get; set ; }
        public string Audience { get; set ; }
        public string TokenExpiry { get ; set ; }
    }

    public interface IJwtToken
    {


        string SecretKey { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        string TokenExpiry { get; set; }

    }
}
