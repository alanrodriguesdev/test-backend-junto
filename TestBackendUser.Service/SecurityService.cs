using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestBackendUser.Domain.Models;

namespace TestBackendUser.Service
{
    public class SecurityService
    {
        public string GerarJwt(Usuario usuario)
        {
            var identityClaims = new ClaimsIdentity();
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome)
            };
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = Environment.GetEnvironmentVariable("Emissor"),
                Audience = Environment.GetEnvironmentVariable("ValidoEm"),
                Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(Environment.GetEnvironmentVariable("ExpiracaoHoras").ToString())),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
