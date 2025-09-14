using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Persistence.IdentityEnitities
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {
        public AppUser() 
        {
            UserDevices = new Collection<UserDevice>();
        }

        [PersonalData]
        public string? Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }

        public Collection<UserDevice>? Activities { get; set; }
        public Collection<UserDevice> UserDevices { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
