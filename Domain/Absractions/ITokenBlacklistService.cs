using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Absractions
{
    public interface ITokenBlacklistService
    {
        Task<bool> IsBlacklistedAsync(Guid tokenId);
        Task BlacklistAsync(Guid tokenId, DateTime? expiry);

    }
}
