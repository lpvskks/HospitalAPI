using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using webNET_2024_aspnet_1.DBContext.Models;

namespace webNET_2024_aspnet_1.Additional_Services.TokenHelpers
{
    public class TokenInteraction
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenInteraction(IConfiguration configuration)
        {
            _secretKey = configuration.GetValue<string>("AppSettings:Secret");
            _issuer = configuration.GetValue<string>("AppSettings:Issuer");
            _audience = configuration.GetValue<string>("AppSettings:Audience");
        }

        public string GenerateToken(Doctor doctor)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, doctor.Id.ToString())
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }

}
