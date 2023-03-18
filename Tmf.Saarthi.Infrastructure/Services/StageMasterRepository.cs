using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Enums;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Response.StageMaster;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services
{
    public class StageMasterRepository : IStageMasterRepository
    {
        private readonly ISqlUtility _sqlUtility;
        private readonly ConnectionStringsOptions _connectionStringsOptions;

        public StageMasterRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
        {
            _sqlUtility = sqlUtility;
            _connectionStringsOptions = connectionStringsOptions.Value;
        }

        public async Task<StageMasterResponseModel> GetStageMasterByStageCode(StageCodeFlag stageCode)
        {
            List<SqlParameter> parameters = new()
            {
                 new SqlParameter("StageCode", stageCode.ToString())
            };

            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetStageDetailByStageCode", parameters);

            StageMasterResponseModel stageMasterResponseModel = new();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    stageMasterResponseModel.StageId = dt.Rows[i]["StageId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["StageId"];
                    stageMasterResponseModel.StageName = dt.Rows[i]["StageName"] == DBNull.Value ? "" : (string)dt.Rows[i]["StageName"];
                    stageMasterResponseModel.IsActive = dt.Rows[i]["IsActive"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsActive"];
                    stageMasterResponseModel.GroupStageid = dt.Rows[i]["GroupStageid"] == DBNull.Value ? 0 : (int)dt.Rows[i]["GroupStageid"];
                    stageMasterResponseModel.StageCode = dt.Rows[i]["StageCode"] == DBNull.Value ? "" : (string)dt.Rows[i]["StageCode"];
                }
            }

            return stageMasterResponseModel;
        }
    }
}
