using Domain.Absractions;
using Microsoft.Extensions.Caching.Distributed;

namespace Persistence.Services
{
    public class TokenBlacklistService(IDistributedCache distributedCache) : ITokenBlacklistService
    {
      
        public async Task BlacklistAsync(Guid tokenId ,DateTime? expiry)
        {
            await  distributedCache.SetStringAsync(tokenId.ToString(), tokenId.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry - DateTime.UtcNow,
            });
        }


        public async Task<bool> IsBlacklistedAsync(Guid tokenId)
        {
           var tokenToCheck = await distributedCache.GetStringAsync(tokenId.ToString());

            return tokenToCheck != null && tokenToCheck.Length > 0;
        }
    }


}
