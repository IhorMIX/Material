using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Material.Web.Options;

public class AuthOption
{
    public class AuthOptions
    {
        public const string ISSUER = "FreeMaterial"; // token publisher
        public const string AUDIENCE = "Material"; // token customer
        const string KEY = "mysupersecret_secretkey!123";   // encryption key
        public const int LIFETIME = 1440; // token liftime
        public const string UserIdCalmName = "UserId";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}