using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MemoCards.Services
{
    public interface ITokenService
    {
        public string CreateToken(params Claim[] claims);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly string _claimTypeName;
        private JwtSecurityTokenHandler _tokenHandler;

        public TokenService(IConfiguration configuration, string claimTypeName = ClaimTypes.Email)
        {
            _configuration = configuration;
            _claimTypeName = claimTypeName;
        }
        public string CreateToken(params Claim[] claims)
        {
            var secretKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Secret"));
            _tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            return _tokenHandler.WriteToken(_tokenHandler.CreateToken(tokenDescriptor));
        }


    }
}
