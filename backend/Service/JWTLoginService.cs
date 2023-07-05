using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace technical_challenge.Service
{
    public class JWTLoginService
    {
        private readonly string _secureKey;
        public JWTLoginService(IConfiguration configuration)
        {
            _secureKey = configuration.GetValue<string>("JwtSettings:SecureKey");
        }

        public (string token, DateTime expirationDate) Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            // Set the expiration date to 1 day from the current date and time
            var expirationDate = DateTime.UtcNow.AddDays(1);

            var payload = new JwtPayload(id.ToString(), null, null, null, expirationDate);
            var securityToken = new JwtSecurityToken(header, payload);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return (token, expirationDate);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
