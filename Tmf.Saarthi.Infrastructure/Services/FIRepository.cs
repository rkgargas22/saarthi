using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.FI;
using Tmf.Saarthi.Infrastructure.Models.Response.FI;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services
{
    public class FIRepository : IFIRepository
    {
        private readonly IHttpService _httpService;
        private readonly ISqlUtility _sqlUtility;
        private readonly ConnectionStringsOptions _connectionStringsOptions;
        private readonly FIRossOptions _fiRossOptions;

        public FIRepository(IHttpService httpService,
                            ISqlUtility sqlUtility,
                            IOptions<ConnectionStringsOptions> connectionStringsOptions,
                            IOptions<FIRossOptions> fiRossOptions)
        {
            _httpService = httpService;
            _sqlUtility = sqlUtility;
            _connectionStringsOptions = connectionStringsOptions.Value;
            _fiRossOptions = fiRossOptions.Value;
        }

        public async Task<InitiateFIDataDetailsResponseModel> GetFIRossInitiateData(long fleetId)
        {
            List<SqlParameter> parameters = new()
            {
                new SqlParameter("FleetId", fleetId)
            };

            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetFIRossInitiateData", parameters);

            InitiateFIDataDetailsResponseModel initiateFIDataDetails = new();

            if (dt.Rows.Count > 0)
            {
                initiateFIDataDetails = ConvertDatatableToObject<InitiateFIDataDetailsResponseModel>(dt);
            }

            return initiateFIDataDetails;
        }

        public async Task<InitiateFIResponseModel> InitiateFI(InitiateFIRequestModel initiateFIRequestModel)
        {
            var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
            Dictionary<string, string> headers = new()
            {
                { "BpNo", "1" },
                { "UserType", "User" }
            };
            JsonDocument result = await _httpService.PostAsync(_fiRossOptions.BaseUrl + _fiRossOptions.InitiateFI, initiateFIRequestModel, headers);

            return JsonSerializer.Deserialize<InitiateFIResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
        }
        
        public async Task UpdateInitiateFIResponse(UpdateInitiateFIRequestModel initiateFIRequestModel)
        {
            long.TryParse(initiateFIRequestModel.BpNo, out long BpNo);
            DateTime.TryParse(initiateFIRequestModel.Timestamp, out DateTime timestamp);

            List<SqlParameter> parameters = new()
            {
                new SqlParameter("QueueId", initiateFIRequestModel.QueueId),
                new SqlParameter("FleetId", initiateFIRequestModel.FleetId),
                new SqlParameter("FanNo", initiateFIRequestModel.FanNo),
                new SqlParameter("BpNo", BpNo),
                new SqlParameter("TransactionId", initiateFIRequestModel.TransactionId),
                new SqlParameter("Timestamp", timestamp),
                new SqlParameter("Message", initiateFIRequestModel.Message),
                new SqlParameter("Status", initiateFIRequestModel.Status),
                new SqlParameter("CreatedBy", initiateFIRequestModel.CreatedBy),
            };

            await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateInitiateFIRossResponse", parameters);
        }

        private static T ConvertDatatableToObject<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();

            DataRow row = dt.Rows[0];
            var objT = Activator.CreateInstance<T>();
            foreach (var pro in properties)
            {
                if (columnNames.Contains(pro.Name.ToLower()))
                {
                    try
                    {
                        if (row[pro.Name] == DBNull.Value)
                        {
                            if (pro.PropertyType == typeof(string))
                            {
                                pro.SetValue(objT, string.Empty);
                            }
                            else
                            {
                                pro.SetValue(objT, default);
                            }
                        }
                        else
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            return objT;
        }
    }
}
