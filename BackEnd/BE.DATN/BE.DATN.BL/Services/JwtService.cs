using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class JwtService
    {
        private readonly string _secretKey;

        private readonly IConfiguration _configuration;

        public JwtService(string secretKey, IConfiguration configuration)
        {
            _secretKey = secretKey;
            _configuration = configuration;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JwtSettings:ValidIssuer"],
                ValidAudience = _configuration["JwtSettings:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }
            catch
            {
                // Token không hợp lệ
                return null;
            }
        }

        public Dictionary<string, string> GetTokenClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);

            var claims = new Dictionary<string, string>();

            foreach (var claim in decodedToken.Claims)
            {
                claims.Add(claim.Type, claim.Value);
            }

            return claims;
        }
    }
}
