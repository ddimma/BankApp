using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankApp.Server.Common.JWTToken
{
	public class JwtTokenBuilder
	{
        private readonly List<Claim> _claims;
        private readonly SymmetricSecurityKey _signingKey;
        private readonly TokenSettings _tokenSettings;

        public JwtTokenBuilder(TokenSettings tokenSettings)
        {
            _claims = new List<Claim>();
            _tokenSettings = tokenSettings;
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey));
        }

        public JwtTokenBuilder AddClaim(string type, string value)
        {
            _claims.Add(new Claim(type, value));
            return this;
        }

        public JwtTokenBuilder AddClaims(IEnumerable<Claim> claims)
        {
            _claims.AddRange(claims);
            return this;
        }

        public string Build()
        {
            var signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenOptions = new JwtSecurityToken(
                claims: _claims,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(_tokenSettings.ExpirationInMinutes)),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}

