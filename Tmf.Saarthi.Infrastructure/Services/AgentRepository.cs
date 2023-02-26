using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Agent;
using Tmf.Saarthi.Infrastructure.Models.Response.Agent;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class AgentRepository : IAgentRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public AgentRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<List<AgentDashBoardResponseModel>> GetAgentDashBoardData(long? agentId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("AgentID", agentId),
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetAgentDashBoardData", parameters);
        List<AgentDashBoardResponseModel> agentDashBoardResponseModels = new List<AgentDashBoardResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AgentDashBoardResponseModel agentDashBoardResponse = new AgentDashBoardResponseModel();
                agentDashBoardResponse.FleedId = Convert.ToString(dt.Rows[i]["FleetID"])??"";
                agentDashBoardResponse.CustomerName = dt.Rows[i]["CustomerName"] == DBNull.Value ? "" : (string)dt.Rows[i]["CustomerName"];
                agentDashBoardResponse.AssignedDateTime = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);
                agentDashBoardResponse.ExpiryDate = Convert.ToDateTime(dt.Rows[i]["ExpiryDate"]);
                agentDashBoardResponse.CaseApplicationStatus = dt.Rows[i]["CaseApplicationStatus"] == DBNull.Value ? "" : (string)dt.Rows[i]["CaseApplicationStatus"];
                agentDashBoardResponse.Status = dt.Rows[i]["Status"] == DBNull.Value ? "" : (string)dt.Rows[i]["Status"];
                agentDashBoardResponseModels.Add(agentDashBoardResponse);
            }
        }
        return agentDashBoardResponseModels;
    }

    public async Task<List<AgentCaseOverViewResponseModel>> GetAgentCaseOverViewData(long fleetID)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", fleetID),
        };
        DataSet ds = await _sqlUtility.ExecuteMultipleCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetAgentCaseOverView", parameters);
        List<AgentCaseOverViewResponseModel> agentCaseOverViewResponseModels = new List<AgentCaseOverViewResponseModel>();
        if(ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AgentCaseOverViewResponseModel agentCaseOverViewResponse = new AgentCaseOverViewResponseModel();
                    agentCaseOverViewResponse.Comments = new List<AgentOverViewCommentResponseModel>();
                    agentCaseOverViewResponse.ID = (int)ds.Tables[0].Rows[i]["ID"];
                    agentCaseOverViewResponse.Status = ds.Tables[0].Rows[i]["Status"] == DBNull.Value ? null : (string)ds.Tables[0].Rows[i]["Status"];
                    agentCaseOverViewResponse.ModuleName = ds.Tables[0].Rows[i]["ModuleName"] == DBNull.Value ? null : (string)ds.Tables[0].Rows[i]["ModuleName"];
                    agentCaseOverViewResponse.LogDateTime = ds.Tables[0].Rows[i]["LogDateTime"] == DBNull.Value ? null : (DateTime)ds.Tables[0].Rows[i]["LogDateTime"];
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        List<AgentOverViewCommentResponseModel> agentOverViewCommentResponseModels = new List<AgentOverViewCommentResponseModel>();
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                        {
                            int logID = (int)ds.Tables[1].Rows[j]["LogId"];
                            if (agentCaseOverViewResponse.ID == logID)
                            {
                                AgentOverViewCommentResponseModel agentOverViewCommentResponse = new AgentOverViewCommentResponseModel();
                                agentOverViewCommentResponse.CommentId = (int)ds.Tables[1].Rows[j]["CommentId"];
                                agentOverViewCommentResponse.Comment = ds.Tables[1].Rows[j]["Comment"] == DBNull.Value ? null : (string)ds.Tables[1].Rows[j]["Comment"];
                                agentOverViewCommentResponseModels.Add(agentOverViewCommentResponse);
                            }
                        }
                        agentCaseOverViewResponse.Comments = agentOverViewCommentResponseModels;
                    }
                    agentCaseOverViewResponseModels.Add(agentCaseOverViewResponse);
                }
            }
        }
        return agentCaseOverViewResponseModels;
    }

    public async Task<AgentCustomerResponseModel> GetAgentCustomerData(string mobileNo)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("MobileNo", mobileNo),
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetAgentCustomerDataByMobileNo", parameters);
        AgentCustomerResponseModel agentCustomerResponseModel = new AgentCustomerResponseModel();
        if (dt.Rows.Count > 0)
        {
            agentCustomerResponseModel.FleedId = (long)dt.Rows[0]["FleetID"];
            agentCustomerResponseModel.FanNo = dt.Rows[0]["FanNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["FanNo"];
            agentCustomerResponseModel.PanNo = dt.Rows[0]["PanNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["PanNo"];
            agentCustomerResponseModel.MobileNo = dt.Rows[0]["MobileNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["MobileNo"];
            agentCustomerResponseModel.BpNo = (long)dt.Rows[0]["BPNumber"];
            agentCustomerResponseModel.FirstName = dt.Rows[0]["FirstName"] != DBNull.Value ? (string)dt.Rows[0]["FirstName"] : "";
            agentCustomerResponseModel.MiddleName = dt.Rows[0]["MiddleName"] != DBNull.Value ? (string)dt.Rows[0]["MiddleName"] : "";
            agentCustomerResponseModel.LastName = dt.Rows[0]["LastName"] != DBNull.Value ? (string)dt.Rows[0]["LastName"] : "";
            agentCustomerResponseModel.Comment = dt.Rows[0]["Comment"] != DBNull.Value ? (string)dt.Rows[0]["Comment"] : "";
        }
        return agentCustomerResponseModel;
    }

    public async Task<List<AgentHsitoryResponseModel>> GetAgentHistoryData(long agentId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", agentId),
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetCaseHistoryDataByFleetID", parameters);
        List<AgentHsitoryResponseModel> agentHsitoryResponseModels = new List<AgentHsitoryResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AgentHsitoryResponseModel agentHsitoryResponse = new AgentHsitoryResponseModel();
                agentHsitoryResponse.BpNo = (long)dt.Rows[i]["BPNumber"];
                agentHsitoryResponse.AgentName = dt.Rows[i]["AgentName"] != DBNull.Value ? (string)dt.Rows[i]["AgentName"] : "";
                agentHsitoryResponse.FleetId = (long)dt.Rows[i]["FleetID"];
                agentHsitoryResponse.Comment = dt.Rows[i]["Comment"] == DBNull.Value ? "" : (string)dt.Rows[i]["Comment"];
                agentHsitoryResponse.LogDate = (DateTime)dt.Rows[i]["LogDate"];
                agentHsitoryResponse.StageName = dt.Rows[i]["StageName"] != DBNull.Value ? (string)dt.Rows[i]["StageName"] : null;
                agentHsitoryResponseModels.Add(agentHsitoryResponse);
            }
        }
        return agentHsitoryResponseModels;
    }

    public async Task<List<AgentRejectedFleetResponseModel>> GetAgentRejectedFleetData(long fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", fleetId),
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetAgentRejectedFleetData", parameters);
        List<AgentRejectedFleetResponseModel> agentRejectedFleetResponseModels = new List<AgentRejectedFleetResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AgentRejectedFleetResponseModel agentRejectedFleetResponse = new AgentRejectedFleetResponseModel();
                agentRejectedFleetResponse.FleetId = (long)dt.Rows[i]["FleetID"];
                agentRejectedFleetResponse.RcNo = dt.Rows[i]["RCNo"] == DBNull.Value ? "" : (string)dt.Rows[i]["RCNo"];
                agentRejectedFleetResponse.OwnerName = dt.Rows[i]["OwnerName"] == DBNull.Value ? "" : (string)dt.Rows[i]["OwnerName"];
                agentRejectedFleetResponse.RegistrationDate = dt.Rows[i]["RegistrationDate"] == DBNull.Value ?  null : Convert.ToDateTime(dt.Rows[i]["RegistrationDate"]);
                agentRejectedFleetResponse.VehicleType = dt.Rows[i]["VehicleType"] == DBNull.Value ? "" : (string)dt.Rows[i]["VehicleType"];
                agentRejectedFleetResponse.VehicleModel = dt.Rows[i]["VehicleModel"] == DBNull.Value ? "" : (string)dt.Rows[i]["VehicleModel"];
                agentRejectedFleetResponse.VehicleCompany = dt.Rows[i]["VehicleCompany"] == DBNull.Value ? "" : (string)dt.Rows[i]["VehicleCompany"];
                agentRejectedFleetResponseModels.Add(agentRejectedFleetResponse);
            }
        }
        return agentRejectedFleetResponseModels;
    }

    public async Task<List<AgentApprovedFleetResponseModel>> GetAgentApprovedFleetData(long fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", fleetId),
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetAgentApporvedFleetData", parameters);
        List<AgentApprovedFleetResponseModel> agentApprovedFleetResponseModels = new List<AgentApprovedFleetResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AgentApprovedFleetResponseModel agentApprovedFleetResponse = new AgentApprovedFleetResponseModel();
                agentApprovedFleetResponse.FleetId = (long)dt.Rows[i]["FleetID"];
                agentApprovedFleetResponse.RcNo = dt.Rows[i]["RCNo"] == DBNull.Value ? "" : (string)dt.Rows[i]["RCNo"];
                agentApprovedFleetResponse.OwnerName = dt.Rows[i]["OwnerName"] == DBNull.Value ? "" : (string)dt.Rows[i]["OwnerName"];
                agentApprovedFleetResponse.RegistrationDate = dt.Rows[i]["RegistrationDate"] == DBNull.Value ? null : Convert.ToDateTime(dt.Rows[i]["RegistrationDate"]);
                agentApprovedFleetResponse.VehicleType = dt.Rows[i]["VehicleType"] == DBNull.Value ? "" : (string)dt.Rows[i]["VehicleType"];
                agentApprovedFleetResponse.VehicleModel = dt.Rows[i]["VehicleModel"] == DBNull.Value ? "" : (string)dt.Rows[i]["VehicleModel"];
                agentApprovedFleetResponse.VehicleCompany = dt.Rows[i]["VehicleCompany"] == DBNull.Value ? "" : (string)dt.Rows[i]["VehicleCompany"];
                agentApprovedFleetResponseModels.Add(agentApprovedFleetResponse);
            }
        }
        return agentApprovedFleetResponseModels;
    }

    public async Task<AgentSalesDeviationResponseModel> GetAgentSalesDeviation(long fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", fleetId),
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetAgentSalesDeviationData", parameters);
        AgentSalesDeviationResponseModel agentSalesDeviationResponse = new AgentSalesDeviationResponseModel();
        if (dt.Rows.Count > 0)
        {
            agentSalesDeviationResponse.FleetId = (long)dt.Rows[0]["FleetID"];
            agentSalesDeviationResponse.ProcessingFee = dt.Rows[0]["ProcessingFee"] == DBNull.Value ? null : (decimal)dt.Rows[0]["ProcessingFee"];
            agentSalesDeviationResponse.StampDuty = dt.Rows[0]["StampDuty"] == DBNull.Value ? null : (decimal)dt.Rows[0]["StampDuty"];
            agentSalesDeviationResponse.IRR = dt.Rows[0]["IRR"] == DBNull.Value ? null : (decimal)dt.Rows[0]["IRR"];
            agentSalesDeviationResponse.AIR = dt.Rows[0]["AIR"] == DBNull.Value ? null : (decimal)dt.Rows[0]["AIR"];
            agentSalesDeviationResponse.RequestedAIR = dt.Rows[0]["RequestedAIR"] == DBNull.Value ? null : (decimal)dt.Rows[0]["RequestedAIR"];
            agentSalesDeviationResponse.RequestedIRR = dt.Rows[0]["RequestedIRR"] == DBNull.Value ? null : (decimal)dt.Rows[0]["RequestedIRR"];
            agentSalesDeviationResponse.RequestedProcessingFees = dt.Rows[0]["RequestedProcessingFees"] == DBNull.Value ? null : (decimal)dt.Rows[0]["RequestedProcessingFees"];
        }
        return agentSalesDeviationResponse;
    }

    public async Task<AgentSalesDeviationUpdateResponseModel> UpdateAgentSalesDeviationData(AgentSalesDeviationRequestModel agentSalesDeviationRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", agentSalesDeviationRequestModel.FleetId),
            new SqlParameter("RequestedProcessingFee", agentSalesDeviationRequestModel.RequestedProcessingFees),
            new SqlParameter("RequestedIRR", agentSalesDeviationRequestModel.RequestedIRR),
            new SqlParameter("RequestedAIR", agentSalesDeviationRequestModel.RequestedAIR),
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateAgentSalesDeviationData", parameters);

        AgentSalesDeviationUpdateResponseModel agentSalesDeviationUpdateResponseModel = new AgentSalesDeviationUpdateResponseModel();
        if (dt.Rows.Count > 0)
        {
            agentSalesDeviationUpdateResponseModel.FleetId = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return agentSalesDeviationUpdateResponseModel;
    }

    public async Task<AgentFIResponseModel> GetAgentFIData(long fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", fleetId),
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetAgentFIData", parameters);
        AgentFIResponseModel agentFIResponseModel = new AgentFIResponseModel();
        if (dt.Rows.Count > 0)
        {
            agentFIResponseModel.FleetId = (long)dt.Rows[0]["FleetId"];
            agentFIResponseModel.Status = dt.Rows[0]["Status"] == DBNull.Value ? "" : (string)dt.Rows[0]["Status"];
            agentFIResponseModel.CreatedDate = dt.Rows[0]["CreatedDate"] == DBNull.Value ? null : (DateTime)dt.Rows[0]["CreatedDate"];
        }
        return agentFIResponseModel;
    }

    public async Task<AssignFleetResponseModel> AssignFleet(AssignFleetRequestModel assignFleetRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetIds", assignFleetRequestModel.FleetIDs),
            new SqlParameter("AgentId", assignFleetRequestModel.AgentId),
            new SqlParameter("Role", assignFleetRequestModel.Role),
            new SqlParameter("UpdatedBy", assignFleetRequestModel.UpdatedBy),
            new SqlParameter("UpdatedDate", assignFleetRequestModel.UpdatedDate)
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_AssignFleetAgent", parameters);
        AssignFleetResponseModel assignFleetResponseModel = new AssignFleetResponseModel();
        if (dt.Rows.Count > 0)
        {
            assignFleetResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
        }
        return assignFleetResponseModel;
    }

    public async Task<AgentCustomerResponseModel> GetAgentCustomerDataByFleetId(long fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("fleetId", fleetId)
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetAgentCustomerDataByFleetID", parameters);
        AgentCustomerResponseModel agentCustomerResponseModel = new AgentCustomerResponseModel();
        if (dt.Rows.Count > 0)
        {
            agentCustomerResponseModel.FleedId = (long)dt.Rows[0]["FleetID"];
            agentCustomerResponseModel.FanNo = dt.Rows[0]["FanNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["FanNo"];
            agentCustomerResponseModel.PanNo = dt.Rows[0]["PanNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["PanNo"];
            agentCustomerResponseModel.MobileNo = dt.Rows[0]["MobileNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["MobileNo"];
            agentCustomerResponseModel.BpNo = (long)dt.Rows[0]["BPNumber"];
            agentCustomerResponseModel.FirstName = dt.Rows[0]["FirstName"] != DBNull.Value ? (string)dt.Rows[0]["FirstName"] : "";
            agentCustomerResponseModel.MiddleName = dt.Rows[0]["MiddleName"] != DBNull.Value ? (string)dt.Rows[0]["MiddleName"] : "";
            agentCustomerResponseModel.LastName = dt.Rows[0]["LastName"] != DBNull.Value ? (string)dt.Rows[0]["LastName"] : "";
            agentCustomerResponseModel.Comment = dt.Rows[0]["Comment"] != DBNull.Value ? (string)dt.Rows[0]["Comment"] : "";
        }
        return agentCustomerResponseModel;
    }
}
