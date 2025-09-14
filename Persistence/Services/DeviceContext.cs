using Domain.Absractions;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace Persistence.Services
{
    public class DeviceContext: IDeviceContext
    {
        private HttpContext _httpContext;
        public string DeviceId { get; set; } = String.Empty;

        public string FingerPrint { get; set; } = String.Empty;

        public DeviceContext(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;
        }
        public string Initialize(Guid userId)
        {
            FingerPrint = GenerateFingerPrint(userId);
            DeviceId = GenerateDeviceId();
            return DeviceId;
        }

        private string GenerateDeviceId()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(FingerPrint));
                DeviceId = Convert.ToHexString(bytes); // .NET 5+; use BitConverter.ToString(bytes).Replace("-", "") for older versions
            }
            //DeviceId = FingerPrint; 
            return DeviceId;
        }

        private string GenerateFingerPrint(Guid userId)
        {
            string fingerprintString = "";
            var ip = _httpContext.Connection.RemoteIpAddress.ToString();
            var userAgent = _httpContext.Request.Headers["User-Agent"].ToString();
            var timezone = _httpContext.Request.Headers["Time-zone"].ToString();

            fingerprintString = userAgent + ip + timezone + userId ;

            FingerPrint = fingerprintString;

            return fingerprintString;
        }
    }
}
