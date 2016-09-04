using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Configurations;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Helpers;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.Common.Models;
using Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlTypes;

namespace Microsoft.Azure.Devices.Applications.RemoteMonitoring.DeviceAdmin.Infrastructure.Repository
{
    public class BYDErrorRepository : IBYDErrorRepository
    {
        private readonly IConfigurationProvider _configurationProvider;

        public BYDErrorRepository(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }
        public async Task<IEnumerable<BYDError>> GetErrorsAsync(DateTime start, DateTime end)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_configurationProvider.GetConfigurationSettingValue("sql.ConnectionString")))
                {

                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from TelemetrySummary where time between @start and @end for json auto", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@start", new SqlDateTime(start)));
                        cmd.Parameters.Add(new SqlParameter("@end", new SqlDateTime(end)));
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            var ds = new DataSet();
                            adapter.Fill(ds);

                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                var sb = new StringBuilder();
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    sb.Append(row[0] as string);
                                }
                                BYDError[] errors = JsonConvert.DeserializeObject<BYDError[]>(sb.ToString());

                                return errors;
                            }

                        }
                    }
                    conn.Close();
                }
                return null;
            }catch(Exception exp)
            {
                return null;
            }
        }
        public async Task<IEnumerable<BYDError>> GetErrorsAsync()
        {
            using (SqlConnection conn = new SqlConnection(_configurationProvider.GetConfigurationSettingValue("sql.ConnectionString")))
            {

                conn.Open();
                using(SqlCommand cmd = new SqlCommand("select * from TelemetrySummary for json auto", conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);

                        if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            var sb = new StringBuilder();
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                sb.Append(row[0] as string);
                            }
                            BYDError[] errors = JsonConvert.DeserializeObject<BYDError[]>(sb.ToString());

                            return errors;
                        }

                    }
                }
                conn.Close();
            }
            return null;
        }


        public async Task<IEnumerable<BYDError>> GetErrorsAsync(string deviceId)
        {
            using (SqlConnection conn = new SqlConnection(_configurationProvider.GetConfigurationSettingValue("sql.ConnectionString")))
            {

                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from TelemetrySummary where DeviceId = @deviceId  for json auto", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@deviceId", deviceId));
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            var sb = new StringBuilder();
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                sb.Append(row[0] as string);
                            }
                            BYDError[] errors = JsonConvert.DeserializeObject<BYDError[]>(sb.ToString());

                            return errors;
                        }

                    }
                }
                conn.Close();
            }
            return null;
        }
    }
}
