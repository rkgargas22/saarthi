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


        public async Task<FiDetailResponseModel> GetFiDetail(long FleetId)
        {
            string fiDevialtion = string.Empty;
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
            new SqlParameter("FleetId", FleetId)
            };
            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFiDetail", parameters);
            FiDetailResponseModel fiDetailResponseModel = new FiDetailResponseModel();
            if (dt.Rows.Count > 0)
            {
                fiDetailResponseModel.FleetID = dt.Rows[0]["FleetID"] == DBNull.Value ? 0 : (Int64)dt.Rows[0]["FleetID"];
                fiDetailResponseModel.VerificationDate = dt.Rows[0]["VerificationDate"] == DBNull.Value ? "" : (string)dt.Rows[0]["VerificationDate"];
                fiDetailResponseModel.FiStatus = dt.Rows[0]["FiStatus"] == DBNull.Value ? "" : (string)dt.Rows[0]["FiStatus"];
                fiDetailResponseModel.CPCStatus = dt.Rows[0]["CPCStatus"] == DBNull.Value ? "" : (string)dt.Rows[0]["CPCStatus"];
                fiDevialtion = dt.Rows[0]["DeviationIds"] == DBNull.Value ? "" : (string)dt.Rows[0]["DeviationIds"];

                if (!string.IsNullOrEmpty(fiDevialtion))
                {
                    string temp = string.Empty;
                    string[] values = fiDevialtion.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        List<SqlParameter> parameters1 = new List<SqlParameter>()
                    {
                        new SqlParameter("DeviationId",Convert.ToInt64(values[i]))
                    };
                        DataTable dt1 = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFiDeviationDetail", parameters1);
                        if (dt1.Rows.Count > 0)
                        {
                            temp += dt1.Rows[0]["Deviations"] == DBNull.Value ? "" : (string)dt1.Rows[0]["Deviations"];
                            if (i + 1 < values.Length)
                            {
                                temp += ", ";
                            }
                        }
                    }
                    fiDetailResponseModel.fiDeviations = temp;
                }
            }

            return fiDetailResponseModel;
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


        public async Task<FiDetailResponseModel> FIRetrigger(FiRetriggerRequestModel fiRetriggerRequestModel)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", fiRetriggerRequestModel.fleetId),
            new SqlParameter("UpdateBy", fiRetriggerRequestModel.UserId),
        };

            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_FiRetrigger", parameters);

            FiDetailResponseModel fiDetailResponseModel = new FiDetailResponseModel();
            if (dt.Rows.Count > 0)
            {
                fiDetailResponseModel.FleetID = Convert.ToInt64(dt.Rows[0]["FleetID"]);
            }

            return fiDetailResponseModel;
        }
    }
}
