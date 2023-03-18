using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.CPCFI;
using Tmf.Saarthi.Infrastructure.Models.Response.CPCFI;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class CPCFIRepository : ICPCFIRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public CPCFIRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<List<CPCDashboardResponseModel>> CPCDashboardData(CPCDashboardRequestModel cPCDashboardRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("AgentId", cPCDashboardRequestModel.AgentId),
            new SqlParameter("Role", cPCDashboardRequestModel.Role)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_CpcFiAgentDashboard", parameters);

        List<CPCDashboardResponseModel> cPCDashboardResponseModels = new List<CPCDashboardResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                CPCDashboardResponseModel cPCDashboardResponseModel = new CPCDashboardResponseModel();
                cPCDashboardResponseModel.FleetID = (long)dt.Rows[i]["FleetID"];
                cPCDashboardResponseModel.CustomerName = dt.Rows[i]["CustomerName"] != DBNull.Value ? (string)dt.Rows[i]["CustomerName"] : string.Empty;
                cPCDashboardResponseModel.AssignDate = dt.Rows[i]["AssignDate"] != DBNull.Value ? (DateTime)dt.Rows[i]["AssignDate"] : null;
                cPCDashboardResponseModel.ExpiryDate = dt.Rows[i]["ExpiryDate"] != DBNull.Value ? (DateTime)dt.Rows[i]["ExpiryDate"] : null;
                cPCDashboardResponseModel.AssignedAgent = dt.Rows[i]["AssignedAgent"] != DBNull.Value ? (string)dt.Rows[i]["AssignedAgent"] : string.Empty;
                cPCDashboardResponseModel.Status = dt.Rows[i]["Status"] != DBNull.Value ? (string)dt.Rows[i]["Status"] : string.Empty;
                cPCDashboardResponseModels.Add(cPCDashboardResponseModel);
            }
        }

        return cPCDashboardResponseModels;
    }

    public async Task<List<CPCDashboardResponseModel>> CPCPoolDashboardData()
    {
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_CpcFiPoolDashboard", null);

        List<CPCDashboardResponseModel> cPCDashboardResponseModels = new List<CPCDashboardResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CPCDashboardResponseModel cPCDashboardResponseModel = new CPCDashboardResponseModel();
                cPCDashboardResponseModel.FleetID = (long)dt.Rows[i]["FleetID"];
                cPCDashboardResponseModel.CustomerName = dt.Rows[i]["CustomerName"] != DBNull.Value ? (string)dt.Rows[i]["CustomerName"] : string.Empty;
                cPCDashboardResponseModel.AssignDate = dt.Rows[i]["AssignDate"] != DBNull.Value ? (DateTime)dt.Rows[i]["AssignDate"] : null;
                cPCDashboardResponseModel.ExpiryDate = dt.Rows[i]["ExpiryDate"] != DBNull.Value ? (DateTime)dt.Rows[i]["ExpiryDate"] : null;
                cPCDashboardResponseModel.AssignedAgent = dt.Rows[i]["AssignedAgent"] != DBNull.Value ? (string)dt.Rows[i]["AssignedAgent"] : string.Empty;
                cPCDashboardResponseModel.Status = dt.Rows[i]["Status"] != DBNull.Value ? (string)dt.Rows[i]["Status"] : string.Empty;
                cPCDashboardResponseModels.Add(cPCDashboardResponseModel);
            }
        }

        return cPCDashboardResponseModels;
    }

    public async Task<List<CPCDeviationListResponseModel>> GetCPCDeviationList()
    {
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFIDeviationStatus", null);

        List<CPCDeviationListResponseModel> cPCDeviationListResponseModels = new List<CPCDeviationListResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CPCDeviationListResponseModel cPCDeviationListResponseModel = new CPCDeviationListResponseModel();
                cPCDeviationListResponseModel.DevId = (int)dt.Rows[i]["DevId"];
                cPCDeviationListResponseModel.Deviation = (string)dt.Rows[i]["Deviation"];
                cPCDeviationListResponseModels.Add(cPCDeviationListResponseModel);
            }
        }

        return cPCDeviationListResponseModels;
    }
}
