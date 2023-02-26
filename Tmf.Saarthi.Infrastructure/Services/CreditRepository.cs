using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Credit;
using Tmf.Saarthi.Infrastructure.Models.Response.Credit;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services
{
    public class CreditRepository : ICreditRepository
    {
        private readonly ISqlUtility _sqlUtility;
        private readonly IHttpService _httpService;
        private readonly ConnectionStringsOptions _connectionStringsOptions;

        public CreditRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, IHttpService httpService, IOptions<InstaVeritaOptions> instaVeritaOptions)
        {
            _sqlUtility = sqlUtility;
            _httpService = httpService;
            _connectionStringsOptions = connectionStringsOptions.Value;
        }

        public async Task<List<CreditDashboardResponseModel>> GetCreditDashboard()
        {
            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getCreditDashboard");

            List<CreditDashboardResponseModel> creditDashboardResponseModelList = new List<CreditDashboardResponseModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CreditDashboardResponseModel creditDashboardResponseModel = new CreditDashboardResponseModel();
                    creditDashboardResponseModel.ApplicationId = dt.Rows[i]["ApplicationId"] == DBNull.Value ? 0 : (Int64)dt.Rows[i]["ApplicationId"];
                    creditDashboardResponseModel.CustomerName = dt.Rows[i]["CustomerName"] == DBNull.Value ? "" : (string)dt.Rows[i]["CustomerName"];
                    creditDashboardResponseModel.AssingedDate = dt.Rows[i]["AssingedDate"] == DBNull.Value ? "" : (string)dt.Rows[i]["AssingedDate"];
                    creditDashboardResponseModel.ExprDate = dt.Rows[i]["ExprDate"] == DBNull.Value ? "" : (string)dt.Rows[i]["ExprDate"];
                    creditDashboardResponseModel.Status = dt.Rows[i]["Status"] == DBNull.Value ? "" : (string)dt.Rows[i]["Status"];
                    creditDashboardResponseModelList.Add(creditDashboardResponseModel);
                }
            }

            return creditDashboardResponseModelList;
        }


        public async Task<List<FiDetailResponseModel>> GetFiDetail(long FleetId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
            new SqlParameter("FleetId", FleetId)
            };
            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFiDetail", parameters);

            List<FiDetailResponseModel> fiDetailResponseModelList = new List<FiDetailResponseModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FiDetailResponseModel fiDetailResponseModel = new FiDetailResponseModel();
                    fiDetailResponseModel.FleetID = dt.Rows[i]["FleetID"] == DBNull.Value ? 0 : (Int64)dt.Rows[i]["FleetID"];
                    fiDetailResponseModel.VerificationDate = dt.Rows[i]["VerificationDate"] == DBNull.Value ? "" : (string)dt.Rows[i]["VerificationDate"];
                    fiDetailResponseModel.FiStatus = dt.Rows[i]["FiStatus"] == DBNull.Value ? "" : (string)dt.Rows[i]["FiStatus"];
                    fiDetailResponseModel.CPCStatus = dt.Rows[i]["CPCStatus"] == DBNull.Value ? "" : (string)dt.Rows[i]["CPCStatus"];
                    fiDetailResponseModel.FiDeviation = dt.Rows[i]["FiDeviation"] == DBNull.Value ? "" : (string)dt.Rows[i]["FiDeviation"];
                    fiDetailResponseModelList.Add(fiDetailResponseModel);
                }
            }

            return fiDetailResponseModelList;
        }

        public async Task<FiDetailResponseModel> UpdateFiDetail(UpdateFiDetailRequestModel updateFiDetailRequestModel)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", updateFiDetailRequestModel.FleetID),
            new SqlParameter("Status", updateFiDetailRequestModel.Status),
            new SqlParameter("Comment", updateFiDetailRequestModel.Comment),
        };

            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateFiStatus", parameters);

            FiDetailResponseModel fiDetailResponseModel = new FiDetailResponseModel();
            if (dt.Rows.Count > 0)
            {
                fiDetailResponseModel.FleetID = Convert.ToInt64(dt.Rows[0]["FleetID"]);
            }

            return fiDetailResponseModel;
        }

    }
}
