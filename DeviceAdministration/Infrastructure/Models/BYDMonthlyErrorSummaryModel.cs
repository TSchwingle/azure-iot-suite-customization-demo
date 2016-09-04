using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Models
{
    public class BYDMonthlyErrorSummaryModel
    {
        public string Date { get; set; }
        public int ErrorCount { get; set; }
    }
}