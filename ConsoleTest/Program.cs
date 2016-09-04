using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Configurations;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.BusinessLogic;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BYDErrorLogic _bydErrorLogic = new BYDErrorLogic(
                new BYDErrorRepository(
                    new ConfigurationProvider()));
            var result = _bydErrorLogic.GetYearlyErrorsSummaryAsync();
            Console.ReadLine();
        }
    }
}
