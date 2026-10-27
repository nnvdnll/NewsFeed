using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NewsWebApi.Models;
public class JwtAuthOptions
{
    public static string Issuer = "IssuerName";
    public static string Audience = "AudienceName";
    public static string Key = "KeyKeyKeyKeyKeyKey1234567890?!+-=";
    public static int LifeTime = 1;
    public static SymmetricSecurityKey SecurityKey => new(Encoding.ASCII.GetBytes(Key));
}
