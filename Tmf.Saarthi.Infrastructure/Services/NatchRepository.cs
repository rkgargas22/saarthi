using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Natch;
using Tmf.Saarthi.Infrastructure.Models.Response.Natch;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class NatchRepository : INatchRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public NatchRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<NatchResponseModel> UpdateNach(NatchRequestModel nachRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", nachRequestModel.FleetID),
            new SqlParameter("AccountNumber", nachRequestModel.AccountNumber),
            new SqlParameter("ConfirmAccountNumber", nachRequestModel.ConfirmAccountNumber),
            new SqlParameter("AccountType", nachRequestModel.AccountType),
            new SqlParameter("IFSCCode", nachRequestModel.IFSCCode),
            new SqlParameter("BankName", nachRequestModel.BankName),
            new SqlParameter("AuthenticationMode", nachRequestModel.AuthenticationMode),
            new SqlParameter("CreatedBy", nachRequestModel.CreatedBy),
            new SqlParameter("IsNach", nachRequestModel.IsNach),
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_UpdateMNach", parameters);

        NatchResponseModel nachResponseModel = new NatchResponseModel();
        if (dt.Rows.Count > 0)
        {
            nachResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
        }
        return nachResponseModel;
    }

    public async Task<NachResponseModelByFleetId> GetNachByFleetId(long FleetId, bool IsEnach)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", FleetId),
            new SqlParameter("IsEnach", IsEnach)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetNachByFleetID", parameters);

        NachResponseModelByFleetId nachResponseModel = new NachResponseModelByFleetId();
        if (dt.Rows.Count > 0)
        {
            nachResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
            nachResponseModel.AccountNumber = dt.Rows[0]["AccountNumber"] == DBNull.Value ? "" : (string)dt.Rows[0]["AccountNumber"];
            nachResponseModel.ConfirmAccountNumber = dt.Rows[0]["ConfirmAccountNumber"] == DBNull.Value ? "" : (string)dt.Rows[0]["ConfirmAccountNumber"];
            nachResponseModel.AccountType = dt.Rows[0]["AccountType"] == DBNull.Value ? "" : (string)dt.Rows[0]["AccountType"];
            nachResponseModel.IFSCCode = dt.Rows[0]["IFSCCode"] == DBNull.Value ? "" : (string)dt.Rows[0]["IFSCCode"];
            nachResponseModel.BankName = dt.Rows[0]["BankName"] == DBNull.Value ? "" : (string)dt.Rows[0]["BankName"];
            nachResponseModel.AuthenticationMode = dt.Rows[0]["AuthenticationMode"] == DBNull.Value ? "" : (string)dt.Rows[0]["AuthenticationMode"];
            nachResponseModel.IsActive = (bool)dt.Rows[0]["IsActive"];
            nachResponseModel.CreatedBy = dt.Rows[0]["CreatedBy"] == DBNull.Value ? "" : (string)dt.Rows[0]["CreatedBy"];
            nachResponseModel.Amount = dt.Rows[0]["Amount"] == DBNull.Value ? null : (decimal)dt.Rows[0]["Amount"];
            nachResponseModel.StartDate = dt.Rows[0]["StartDate"] == DBNull.Value ? null : (DateTime)dt.Rows[0]["StartDate"];
            nachResponseModel.EndDate = dt.Rows[0]["EndDate"] == DBNull.Value ? null : (DateTime)dt.Rows[0]["EndDate"];
            nachResponseModel.Frequency = dt.Rows[0]["Frequency"] == DBNull.Value ? "" : (string)dt.Rows[0]["Frequency"];
            nachResponseModel.PurposeOfManadate = dt.Rows[0]["PurposeOfManadate"] == DBNull.Value ? "" : (string)dt.Rows[0]["PurposeOfManadate"];
            nachResponseModel.IsEnach = (bool)dt.Rows[0]["IsEnach"];

            nachResponseModel.Status = dt.Rows[0]["Status"] == DBNull.Value ? "" : (string)dt.Rows[0]["Status"];
            nachResponseModel.TimeSlotDate = dt.Rows[0]["TimeSlotDate"] == DBNull.Value ? null : (DateTime)dt.Rows[0]["TimeSlotDate"];
            nachResponseModel.UMRN = dt.Rows[0]["UMRN"] == DBNull.Value ? "" : (string)dt.Rows[0]["UMRN"];
            nachResponseModel.EmandateId = dt.Rows[0]["EmandateId"] == DBNull.Value ? "" : (string)dt.Rows[0]["EmandateId"];
            nachResponseModel.EmandateDate = dt.Rows[0]["EmandateDate"] == DBNull.Value ? null : (DateTime)dt.Rows[0]["EmandateDate"];
            nachResponseModel.MaxAmount = dt.Rows[0]["MaxAmount"] == DBNull.Value ? null : (decimal)dt.Rows[0]["MaxAmount"];
            nachResponseModel.CorporateName = dt.Rows[0]["CorporateName"] == DBNull.Value ? "" : (string)dt.Rows[0]["CorporateName"];
            nachResponseModel.UtilityNo = dt.Rows[0]["UtilityNo"] == DBNull.Value ? "" : (string)dt.Rows[0]["UtilityNo"];
        }

        return nachResponseModel;
    }

    public async Task<List<DropdownResponseModel>> GetBank()
    {
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getBank");

        List<DropdownResponseModel> bankResponseModelList = new List<DropdownResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropdownResponseModel bankResponseModel = new DropdownResponseModel();
                bankResponseModel.Id = (int)dt.Rows[i]["ID"];
                bankResponseModel.DisplayName = dt.Rows[i]["DISPLAYNAME"] == DBNull.Value ? "" : (string)dt.Rows[i]["DISPLAYNAME"];
                bankResponseModelList.Add(bankResponseModel);
            }
        }

        return bankResponseModelList;
    }

    public async Task<List<DropdownResponseModel>> GetState(int bankId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("bankId", bankId)
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getState", parameters);

        List<DropdownResponseModel> stateResponseModelList = new List<DropdownResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropdownResponseModel stateResponseModel = new DropdownResponseModel();
                stateResponseModel.Id = (int)dt.Rows[i]["ID"];
                stateResponseModel.DisplayName = dt.Rows[i]["DISPLAYNAME"] == DBNull.Value ? "" : (string)dt.Rows[i]["DISPLAYNAME"];
                stateResponseModelList.Add(stateResponseModel);
            }
        }

        return stateResponseModelList;
    }

    public async Task<List<DropdownResponseModel>> GetCity(int stateId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("stateId", stateId)
        };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getCity", parameters);

        List<DropdownResponseModel> cityResponseModelList = new List<DropdownResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropdownResponseModel cityResponseModel = new DropdownResponseModel();
                cityResponseModel.Id = (int)dt.Rows[i]["ID"];
                cityResponseModel.DisplayName = dt.Rows[i]["DISPLAYNAME"] == DBNull.Value ? "" : (string)dt.Rows[i]["DISPLAYNAME"];
                cityResponseModelList.Add(cityResponseModel);
            }
        }

        return cityResponseModelList;
    }

    public async Task<List<DropdownResponseModel>> GetBranch(int BankId, int StateId, int CityId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("bankId", BankId),
            new SqlParameter("stateId", StateId),
            new SqlParameter("cityId", CityId)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getBranch", parameters);

        List<DropdownResponseModel> branchResponseModelList = new List<DropdownResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropdownResponseModel branchResponseModel = new DropdownResponseModel();
                branchResponseModel.Id = (int)dt.Rows[i]["ID"];
                branchResponseModel.DisplayName = dt.Rows[i]["DISPLAYNAME"] == DBNull.Value ? "" : (string)dt.Rows[i]["DISPLAYNAME"];
                branchResponseModelList.Add(branchResponseModel);
            }
        }

        return branchResponseModelList;
    }

    public async Task<NachResponseModelIFSC> GetIFSCCode(int BankId, int StateId, int CityId, int BranchId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("bankId", BankId),
            new SqlParameter("stateId", StateId),
            new SqlParameter("cityId", CityId),
            new SqlParameter("branchId", BranchId)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getBankIFSC", parameters);

        NachResponseModelIFSC nachResponseModel = new NachResponseModelIFSC();
        if (dt.Rows.Count > 0)
        {
            nachResponseModel.IFSCCode = dt.Rows[0]["ifsccode"] == DBNull.Value ? "" : (string)dt.Rows[0]["ifsccode"];
        }
        return nachResponseModel;
    }

    public async Task<NatchResponseModel> AddNach(AddNatchRequestModel nachRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", nachRequestModel.FleetID),
            new SqlParameter("Amount", nachRequestModel.Amount),
            new SqlParameter("StartDate", nachRequestModel.StartDate),
            new SqlParameter("EndDate", nachRequestModel.EndDate),
            new SqlParameter("Frequency", nachRequestModel.Frequency),
            new SqlParameter("PurposeOfMandate", nachRequestModel.PurposeOfManadate),
            new SqlParameter("IsEnach", nachRequestModel.IsEnach),
            new SqlParameter("MaxAmount", nachRequestModel.MaxAmount)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_InsertNach", parameters);

        NatchResponseModel nachResponseModel = new NatchResponseModel();
        if (dt.Rows.Count > 0)
        {
            nachResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
        }
        return nachResponseModel;
    }

    public async Task<NatchResponseModel> UpdateNachStatus(UpdateNatchStatusRequestModel nachStatusModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", nachStatusModel.FleetID),
            new SqlParameter("Status", nachStatusModel.Status),
            new SqlParameter("IsNach", nachStatusModel.IsNach),
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateNachStatus", parameters);

        NatchResponseModel nachResponseModel = new NatchResponseModel();
        if (dt.Rows.Count > 0)
        {
            nachResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
        }
        return nachResponseModel;
    }

    public async Task<NatchResponseModel> UpdateTimeSlotStatus(UpdateNatchTimeSlotRequestModel updateNachTimeSlotModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", updateNachTimeSlotModel.FleetID),
            new SqlParameter("IsNach", updateNachTimeSlotModel.IsNach),
            new SqlParameter("TimeSlot", updateNachTimeSlotModel.TimeSlot),
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateTimeSlotStatus", parameters);

        NatchResponseModel nachResponseModel = new NatchResponseModel();
        if (dt.Rows.Count > 0)
        {
            nachResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
        }
        return nachResponseModel;
    }

    public async Task<NatchStatusAndTimeslotResponseModel> GetTimeSlotAndStatusDate(long FleetId, bool IsEnach)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", FleetId),
            new SqlParameter("IsNach", IsEnach)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetTimeSlotAndStatusDate", parameters);

        NatchStatusAndTimeslotResponseModel nachStatusAndTimeslotResponseModel = new NatchStatusAndTimeslotResponseModel();
        if (dt.Rows.Count > 0)
        {
            nachStatusAndTimeslotResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
            nachStatusAndTimeslotResponseModel.Status =  dt.Rows[0]["Status"] == DBNull.Value ? null : (bool)dt.Rows[0]["Status"];
            nachStatusAndTimeslotResponseModel.TimeSlotDate = dt.Rows[0]["TimeSlotDate"] == DBNull.Value ? null : (DateTime)dt.Rows[0]["TimeSlotDate"];
        }

        return nachStatusAndTimeslotResponseModel;
    }

    public async Task<NatchResponseModel> UpdateNachMandate(NatchMandateRequestModel natchMandateRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", natchMandateRequestModel.FleetID),
            new SqlParameter("EmandateId", natchMandateRequestModel.EmandateId),
            new SqlParameter("EmandateDate", natchMandateRequestModel.EmandateDate),
            new SqlParameter("UMRN", natchMandateRequestModel.UMRN),
            new SqlParameter("CorporateName", natchMandateRequestModel.CorporateName),
            new SqlParameter("UtilityNo", natchMandateRequestModel.UtilityNo)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_UpdateNachMandate", parameters);

        NatchResponseModel nachResponseModel = new NatchResponseModel();
        if (dt.Rows.Count > 0)
        {
            nachResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
        }
        return nachResponseModel;
    }
}
