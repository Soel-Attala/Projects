using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityAPI.Models.DataModels;

namespace UniversityAPI.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserToken userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MM dd yyyy HH: mm:ss:tt")),
            };

            if (userAccounts.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            else if (userAccounts.UserName == "User 1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("User Only", "User 1"));
            }

            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserToken userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        public static UserToken GenTokenKey(UserToken model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new UserToken();
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                //obtain secret key

                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSinginKey);

                Guid Id;

                //Expires in one day

                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                //Validity token
                UserToken.Validity = expireTime.TimeOfDay;

                //Generate our JWT
                var jwToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256));


                UserToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                UserToken.UserName = model.UserName;
                UserToken.Id = model.Id;
                UserToken.GuId = Id;
                return UserToken;

            }
            catch (Exception ex)
            {
                throw new Exception("Error generating jwt", ex);
            }
        }
    }
}
