using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Nach;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;
using Tmf.Saarthi.Infrastructure.Models.Response.Nach;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class NachRepository : INachRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly IHttpService _httpService;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public NachRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, IHttpService httpService, IOptions<InstaVeritaOptions> instaVeritaOptions)
    {
        _sqlUtility = sqlUtility;
        _httpService = httpService;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<NachResponseModel> UpdateNach(NachRequestModel nachRequestModel)
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

        NachResponseModel nachResponseModel = new NachResponseModel();
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
    public async Task<NachResponseModel> AddNach(AddNachRequestModel nachRequestModel)
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
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_InsertNach", parameters);

        NachResponseModel nachResponseModel = new NachResponseModel();
        if (dt.Rows.Count > 0)
        {
            nachResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
        }
        return nachResponseModel;
    }
    public async Task<NachResponseModel> UpdateNachStatus(UpdateNachStatusRequestModel nachStatusModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", nachStatusModel.FleetID),
            new SqlParameter("Status", nachStatusModel.Status),
            new SqlParameter("IsNach", nachStatusModel.IsNach),
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateNachStatus", parameters);

        NachResponseModel nachResponseModel = new NachResponseModel();
        if (dt.Rows.Count > 0)
        {
            nachResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
        }
        return nachResponseModel;
    }
    public async Task<NachResponseModel> UpdateTimeSlotStatus(UpdateNachTimeSlotRequestModel updateNachTimeSlotModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", updateNachTimeSlotModel.FleetID),
            new SqlParameter("IsNach", updateNachTimeSlotModel.IsNach),
            new SqlParameter("TimeSlot", updateNachTimeSlotModel.TimeSlot),
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateTimeSlotStatus", parameters);

        NachResponseModel nachResponseModel = new NachResponseModel();
        if (dt.Rows.Count > 0)
        {
            nachResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
        }
        return nachResponseModel;
    }
    public async Task<NachStatusAndTimeslotResponseModel> GetTimeSlotAndStatusDate(long FleetId, bool IsEnach)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", FleetId),
            new SqlParameter("IsNach", IsEnach)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetTimeSlotAndStatusDate", parameters);

        NachStatusAndTimeslotResponseModel nachStatusAndTimeslotResponseModel = new NachStatusAndTimeslotResponseModel();
        if (dt.Rows.Count > 0)
        {
            nachStatusAndTimeslotResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
            nachStatusAndTimeslotResponseModel.Status =  dt.Rows[0]["Status"] == DBNull.Value ? null : (bool)dt.Rows[0]["Status"];
            nachStatusAndTimeslotResponseModel.TimeSlotDate = dt.Rows[0]["TimeSlotDate"] == DBNull.Value ? null : (DateTime)dt.Rows[0]["TimeSlotDate"];
        }

        return nachStatusAndTimeslotResponseModel;
    }
}
