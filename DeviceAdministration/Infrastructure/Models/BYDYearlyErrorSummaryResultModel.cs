using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Models
{
    public class BYDYearlyErrorSummaryResultModel
    {
        public int Year { get; set; }
        public IList<BYDMonthlyErrorSummaryModel> Data { get; set; }
    }
}