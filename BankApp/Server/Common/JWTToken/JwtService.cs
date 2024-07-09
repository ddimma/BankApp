using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BankApp.Server.Common.JWTToken
{
    public interface IJwtService
    {
        string GetUserIdFromToken(string authorizationHeader);
    }

    public class JwtService : IJwtService
    {
        public readonly TokenSettings tokenSettings;

        public JwtService(TokenSettings tokenSettings)
        {
            ArgumentNullException.ThrowIfNull(tokenSettings);

            this.tokenSettings = tokenSettings;
        }

        public string CreateAuthToken(string userId, string username, string[] roles)
        {
            try
            {
                return new JwtTokenBuilder(tokenSettings)
                    .AddClaim(Constants.UserIdClaimName, userId)
                    .AddClaim(Constants.UsernameClaimName, username)
                    .AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)))
                    .Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing the JWT token:");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public string GetUserIdFromToken(string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("bearer "))
            {
                return null;
            }

            var token = authorizationHeader.Substring("bearer ".Length);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null)
            {
                return null;
            }

            var userIdClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == Constants.UserIdClaimName);

            return userIdClaim?.Value;
        }
    }
}

