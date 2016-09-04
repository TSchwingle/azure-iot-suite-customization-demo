using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Models;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Repository;
using System.Data.SqlTypes;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.BusinessLogic
{
    public class BYDErrorLogic : IBYDErrorLogic
    {
        IBYDErrorRepository _errorRepository = null;
        public BYDErrorLogic(IBYDErrorRepository errorRepository)
        {
            _errorRepository = errorRepository;
        }
        public async Task<IEnumerable<BYDError>> GetErrorsAsync()
        {
            var result = await _errorRepository.GetErrorsAsync();
            return result;  
        }
        public async Task<IEnumerable<BYDError>> GetLastErrorsAsync()
        {
            //假設設備一定會定時送上數據，所以這裡一定有每個設備的最新狀態
            //為了方便，這裡我只找前五天的資料，實際上也許需要新增一個新的Method來取得最後一筆狀態碼
            var results = await _errorRepository.GetErrorsAsync(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddMinutes(5));

            var grouping = from item in results
                            group item by item.DeviceId;
            var groups = from gp in grouping
                         select gp.OrderByDescending(item => item.Time).Take(1).FirstOrDefault();

            return groups;
        }
        public async Task<BYDYearlyErrorSummaryResultModel> GetYearlyErrorsSummaryAsync()
        {
            var errors = await _errorRepository.GetErrorsAsync(
                                    new DateTime(DateTime.UtcNow.Year, 1, 1),
                                    new DateTime(DateTime.UtcNow.Year, 12, 31)
                );
            var groups = from error in errors
                      group error by string.Format("{0}/{1:00}", DateTime.UtcNow.Year, error.Time.Month);
            var monthlySummary = from gp in groups
                         select new BYDMonthlyErrorSummaryModel
                         {
                             Date = gp.Key,
                             ErrorCount = gp.Count()
                         };
            var allMonthes = new int[] { 1,2,3,4,5,6,7,8,9,10,11,12};
            
            var result = new BYDYearlyErrorSummaryResultModel
            {
                Year = DateTime.UtcNow.Year,
                Data = monthlySummary.ToList()
            };

            var monthes = result.Data.Select(d => int.Parse(d.Date.Split('/')[1])).ToArray();


            var missingMonthes = from month in allMonthes
                                 where !monthes.Contains(month)
                                 select month;
            //allMonthes.SkipWhile(i => monthes.Contains(i));
            var missingObj = missingMonthes.Select(m => new BYDMonthlyErrorSummaryModel
            {
                Date = string.Format("{0}/{1:00}",DateTime.UtcNow.Year,m),
                ErrorCount = 0
            });
            SortedList<string, BYDMonthlyErrorSummaryModel> slist = new SortedList<string, BYDMonthlyErrorSummaryModel>();
            foreach(var addedMonth in result.Data)
            {
                slist.Add(addedMonth.Date, addedMonth);
            }            
            foreach(var missing in missingObj)
            {
                slist.Add(missing.Date, missing);
            }
            result.Data = slist.Select(x => x.Value).ToList();
            return result;
        }
        public async Task<IEnumerable<BYDError>> GetErrorsAsync(string deviceId)
        {
            var result = await _errorRepository.GetErrorsAsync(deviceId);
            return result;
        }
    }
}
