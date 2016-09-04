using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Models
{
    public class BYDError
    {
        public string DeviceId { get; set; }
        public int ErrorCode { get; set; }
        public DateTime Time { get; set; }
        public int StatusCode { get; set; }
    }
}
