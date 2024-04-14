using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FeatureTestsFramework.SOA.Tests.Logic.Token
{
    public interface IFakeJwtTokenGenerator
    {
        string GenerateToken();
        string GenerateToken(IEnumerable<Claim> claims);
    }

    public class FakeJwtTokenGenerator: IFakeJwtTokenGenerator
    {
        public const string Secret = "FakeTokenSuperSecretFakeTokenSuperSecret";
        public const string Issuer = "https://complianceMockIssuer";

        public string GenerateToken() => GenerateToken(Array.Empty<Claim>());

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenExpiry = DateTime.UtcNow.AddHours(1);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = Issuer,
                Subject = new ClaimsIdentity(claims),
                NotBefore = tokenExpiry.AddHours(-1),
                Expires = tokenExpiry,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var securityTokenHandler = new JwtSecurityTokenHandler();
            var token = securityTokenHandler.CreateToken(tokenDescriptor);

            return securityTokenHandler.WriteToken(token);
        }
    }
}
