using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Models;
namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Repository
{
    public interface IBYDErrorRepository
    {
        Task<IEnumerable<BYDError>> GetErrorsAsync();
        Task<IEnumerable<BYDError>> GetErrorsAsync(string deviceId);

        Task<IEnumerable<BYDError>> GetErrorsAsync(DateTime start, DateTime end);
    }
}
