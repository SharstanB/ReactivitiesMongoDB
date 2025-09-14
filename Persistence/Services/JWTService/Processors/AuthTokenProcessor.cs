using Domain.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.IdentityEnitities;
using Persistence.Services.JWTService.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Persistence.Services.JWTService.Processors
{
    public class AuthTokenProcessor : IAuthTokenProcessor
    {
        private readonly JWTOptions _jwtOptions;

        private readonly IHttpContextAccessor _httpContextAccessor; 

        public AuthTokenProcessor(IOptions<JWTOptions> jwtOptions , IHttpContextAccessor httpContextAccessor)
        {
            _jwtOptions = jwtOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public (string jwttoken , DateTime expiresAtUtc) GenerateToken (AppUser user)
        {

            var stringKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

            var credentials = new SigningCredentials(stringKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.ToString())

            };

            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationTimeInMinutes);

            var token = new JwtSecurityToken(issuer: _jwtOptions.Issuer, 
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expires ,
                signingCredentials: credentials);

            var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);

            return (jwttoken, expires);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);                               
        }

        // store the tocken in cookies which is one of the recommend places to store the tocken
        public void WriteTokenAsHttpOnlyCookies(string cookiesName , string token, DateTime expiration)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookiesName, token, new CookieOptions
            {
                HttpOnly = true,
                Expires = expiration,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }

    }
}
