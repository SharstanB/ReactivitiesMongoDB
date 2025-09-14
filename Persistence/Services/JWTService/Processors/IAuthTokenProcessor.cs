using Persistence.IdentityEnitities;

namespace Domain.IServices
{
    public interface IAuthTokenProcessor
    {
        (string jwttoken, DateTime expiresAtUtc) GenerateToken(AppUser user);
        string GenerateRefreshToken();

        void WriteTokenAsHttpOnlyCookies(string cookiesName, string token, DateTime expiration);

    }
}
