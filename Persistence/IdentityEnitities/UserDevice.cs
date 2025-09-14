using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IdentityEnitities
{
    public class UserDevice : IBaseEntity
    {
        public Guid Id { get ; set; }

        public string? RefreshToken { get; set; }
        public string? DeviceId { get; set; }

        public string? FingerPrint { get; set; }

        public DateTime? RefreshTokenExpiration { get; set; }
        public DateTime CreatedAt { get; set ; }
        public DateTime? DeletedAt { get; set; }

        public AppUser User { get; set; }
        public Guid UserId { get; set; }
    }
}
