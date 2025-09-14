using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services.JWTService.Options
{
    public class JWTOptions
    {
        public const string JWTOptionsKey = "JWTOptions";
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpirationTimeInMinutes { get; set; }
    }
}
