using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.CPCFacility;
using Tmf.Saarthi.Infrastructure.Models.Request.CPCFI;
using Tmf.Saarthi.Infrastructure.Models.Response.CPCFacility;
using Tmf.Saarthi.Infrastructure.Models.Response.CPCFI;
using Tmf.Saarthi.Infrastructure.Models.Response.Natch;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class CPCFacilityRepository : ICPCFacilityRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly IHttpService _httpService;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public CPCFacilityRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, IHttpService httpService, IOptions<InstaVeritaOptions> instaVeritaOptions)
    {
        _sqlUtility = sqlUtility;
        _httpService = httpService;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<List<DealDetailsResponseModel>> GetDealDetails(object fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
        new SqlParameter("FleetId", fleetId)
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFacilityDealDetail", parameters);
        List<DealDetailsResponseModel> dealDetailsResponseModelList = new List<DealDetailsResponseModel>();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DealDetailsResponseModel dealDetailsResponseModel = new DealDetailsResponseModel();
                dealDetailsResponseModel.FleetID = (Int64)dt.Rows[0]["FleetID"];
                dealDetailsResponseModel.LoanAmount = dt.Rows[0]["LoanAmount"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["LoanAmount"];
                dealDetailsResponseModel.AIR = dt.Rows[0]["AIR"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["AIR"];
                dealDetailsResponseModel.IRR = dt.Rows[0]["IRR"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["IRR"];
                dealDetailsResponseModel.ProcessingFeeAmount = dt.Rows[0]["ProcessingFeeAmount"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["ProcessingFeeAmount"];
                dealDetailsResponseModel.StampDutyAmount = dt.Rows[0]["StampDutyAmount"] == DBNull.Value ? 0 : (decimal)dt.Rows[0]["StampDutyAmount"];
                dealDetailsResponseModel.FacilityNumber = dt.Rows[0]["FacilityNumber"] == DBNull.Value ? "" : (string)dt.Rows[0]["FacilityNumber"];
                dealDetailsResponseModel.FacilityDate = dt.Rows[0]["FacilityDate"] == DBNull.Value ? null : (DateTime)dt.Rows[0]["FacilityDate"];
                dealDetailsResponseModel.FleetCount = dt.Rows[0]["FleetCount"] == DBNull.Value ? 0 : (int)dt.Rows[0]["FleetCount"];
                dealDetailsResponseModelList.Add(dealDetailsResponseModel);

            }
        }

        return dealDetailsResponseModelList;
    }

    public async Task<List<ApprovedFleetResponseModel>> GetApprovedFleet(long fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
        new SqlParameter("FleetId", fleetId)
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFacilityVehicleDetail", parameters);
        List<ApprovedFleetResponseModel> approvedFleetResponseModelList = new List<ApprovedFleetResponseModel>();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ApprovedFleetResponseModel approvedFleetResponseModel = new ApprovedFleetResponseModel();
                approvedFleetResponseModel.VehicleID = (Int64)dt.Rows[0]["VehicleID"];
                approvedFleetResponseModel.VehicleNo = dt.Rows[0]["VehicleNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["VehicleNo"];
                approvedFleetResponseModel.Comment = dt.Rows[0]["Comment"] == DBNull.Value ? "" : (string)dt.Rows[0]["Comment"];
                approvedFleetResponseModel.ApprovedBy = dt.Rows[0]["ApprovedBy"] == DBNull.Value ? "" : (string)dt.Rows[0]["ApprovedBy"];
                approvedFleetResponseModelList.Add(approvedFleetResponseModel);
            }
        }

        return approvedFleetResponseModelList;
    }

    public async Task<List<NachDetailsResponseModel>> GetNachDetails(long fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
        new SqlParameter("FleetId", fleetId),
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetPAndENach", parameters);
        List<NachDetailsResponseModel> nachDetailsResponseModelList = new List<NachDetailsResponseModel>();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                NachDetailsResponseModel nachDetailsResponseModel = new NachDetailsResponseModel();
                nachDetailsResponseModel.NachId = (int)dt.Rows[i]["NachId"];
                nachDetailsResponseModel.FleetId = (Int64)dt.Rows[i]["FleetId"];
                nachDetailsResponseModel.IsEnach = dt.Rows[i]["IsEnach"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsEnach"];
                nachDetailsResponseModel.UMRN = dt.Rows[i]["UMRN"] == DBNull.Value ? "" : (string)dt.Rows[i]["UMRN"];
                nachDetailsResponseModel.EMandateId = dt.Rows[i]["EMandateId"] == DBNull.Value ? "" : (string)dt.Rows[i]["EMandateId"];
                nachDetailsResponseModel.EDate = dt.Rows[i]["EDate"] == DBNull.Value ? "" : (string)dt.Rows[i]["EDate"];
                nachDetailsResponseModel.Amount = dt.Rows[i]["Amount"] == DBNull.Value ? i : (decimal)dt.Rows[i]["Amount"];
                nachDetailsResponseModel.Frequency = dt.Rows[i]["Frequency"] == DBNull.Value ? "" : (string)dt.Rows[i]["Frequency"];
                nachDetailsResponseModel.BankName = dt.Rows[i]["BankName"] == DBNull.Value ? "" : (string)dt.Rows[i]["BankName"];
                nachDetailsResponseModel.AccountType = dt.Rows[i]["AccountType"] == DBNull.Value ? "" : (string)dt.Rows[i]["AccountType"];
                nachDetailsResponseModel.AccountNumber = dt.Rows[i]["AccountNumber"] == DBNull.Value ? "" : (string)dt.Rows[i]["AccountNumber"];
                nachDetailsResponseModel.IFSCCode = dt.Rows[i]["IFSCCode"] == DBNull.Value ? "" : (string)dt.Rows[i]["IFSCCode"];
                nachDetailsResponseModelList.Add(nachDetailsResponseModel);
            }
        }

        return nachDetailsResponseModelList;
    }

    public async Task<InwardFIDetailResponseModel> GetInwardFIDetail(long fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
        new SqlParameter("FleetId", fleetId),
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_CPCFiDetails", parameters);
        InwardFIDetailResponseModel inwardFIDetailResponseModel = new InwardFIDetailResponseModel();

        if (dt.Rows.Count > 0)
        {
            inwardFIDetailResponseModel.FiID = (Int64)dt.Rows[0]["FiID"];
            inwardFIDetailResponseModel.FleetId = (Int64)dt.Rows[0]["FleetId"];
            inwardFIDetailResponseModel.FIStatus = dt.Rows[0]["FIStatus"] == DBNull.Value ? "" : (string)dt.Rows[0]["FIStatus"];
            inwardFIDetailResponseModel.VerificationDate = dt.Rows[0]["VerificationDate"] == DBNull.Value ? "" : (string)dt.Rows[0]["VerificationDate"];
        }

        return inwardFIDetailResponseModel;
    }

    public async Task<List<DropdownResponseModel>> GetInwardFIDeviationList()
    {
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFIDeviationList");
        List<DropdownResponseModel> dropdownResponseModelList = new List<DropdownResponseModel>();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DropdownResponseModel dropdownResponseModel = new DropdownResponseModel();
                dropdownResponseModel.Id = Convert.ToInt64(dt.Rows[i]["ID"]);
                dropdownResponseModel.DisplayName = dt.Rows[i]["DISPLAYNAME"] == DBNull.Value ? "" : (string)dt.Rows[i]["DISPLAYNAME"]; ;
                dropdownResponseModelList.Add(dropdownResponseModel);
            }
        }

        return dropdownResponseModelList;
    }

    public async Task<UpdateCPCFleetDeviationResponseModel> UpdateCPCFleetDeviation(UpdateCPCFleetDeviationRequestModel updateCPCFleetDeviationRequestModel)
    {
        string deviationIds = string.Empty;
        if (!updateCPCFleetDeviationRequestModel.CPCCheckStatus)
        {
            for (int i = 1; i <= updateCPCFleetDeviationRequestModel.DeviationId.Length; i++)
            {
                if (updateCPCFleetDeviationRequestModel.DeviationId[i - 1] > 0)
                {
                    deviationIds += updateCPCFleetDeviationRequestModel.DeviationId[i - 1];
                    if (i < updateCPCFleetDeviationRequestModel.DeviationId.Length)
                    {
                        deviationIds += ",";
                    }
                }
            }
        }

        List<SqlParameter> parameters = new List<SqlParameter>()
        {
        new SqlParameter("FleetId", updateCPCFleetDeviationRequestModel.FleetId),
        new SqlParameter("DeviationIds", deviationIds),
        new SqlParameter("Comment", updateCPCFleetDeviationRequestModel.Comment),
        new SqlParameter("IsCPCChecked", updateCPCFleetDeviationRequestModel.CPCCheckStatus),
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_UpdateCPCFiInward", parameters);

        UpdateCPCFleetDeviationResponseModel updateCPCFleetDeviationResponseModel = new UpdateCPCFleetDeviationResponseModel();
        if (dt.Rows.Count > 0)
        {
            updateCPCFleetDeviationResponseModel.FleetId = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return updateCPCFleetDeviationResponseModel;
    }

    public async Task<List<CPCDashboardResponseModel>> CPCDashboardData(CPCDashboardRequestModel cPCDashboardRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
    {
        new SqlParameter("AgentId", cPCDashboardRequestModel.AgentId),
        new SqlParameter("Role", cPCDashboardRequestModel.Role)
    };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_CpcFcAgentDashboard", parameters);

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

    public async Task<List<CPCDashboardResponseModel>> CPCPoolDashboardData()
    {
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_CpcFcPoolDashboard", null);

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

}
