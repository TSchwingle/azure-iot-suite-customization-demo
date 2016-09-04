using Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.BusinessLogic
{
    public interface IBYDErrorLogic
    {
        Task<IEnumerable<BYDError>> GetErrorsAsync();
        Task<BYDYearlyErrorSummaryResultModel> GetYearlyErrorsSummaryAsync();
        Task<IEnumerable<BYDError>> GetErrorsAsync(string deviceId);
    }
}
