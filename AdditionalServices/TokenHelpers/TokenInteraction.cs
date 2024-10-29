using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.Models;

namespace webNET_2024_aspnet_1.Additional_Services.TokenHelpers
{
    public class TokenInteraction
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private JwtSecurityTokenHandler _tokenHandler;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenInteraction(IConfiguration configuration, IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _secretKey = configuration.GetValue<string>("AppSettings:Secret");
            _issuer = configuration.GetValue<string>("AppSettings:Issuer");
            _audience = configuration.GetValue<string>("AppSettings:Audience");
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string GenerateToken(Doctor doctor)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, doctor.Id.ToString())
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

        public string GetTokenFromHeader()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                string authorizationHeader = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                {
                    return authorizationHeader.Substring("Bearer ".Length);
                }
                return null;
            }
        }


        public string GetIdFromToken(string token)
        {
            var jwtToken = _tokenHandler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null) {
                Console.WriteLine(jwtToken);
            }

            var doctorId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            return doctorId;
        }
    }

}
