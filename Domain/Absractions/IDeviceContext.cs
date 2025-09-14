using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Absractions
{
    public interface IDeviceContext
    {
        string DeviceId { get; set; }
        string FingerPrint { get; set; }
        string Initialize(Guid userId);
    }
}
