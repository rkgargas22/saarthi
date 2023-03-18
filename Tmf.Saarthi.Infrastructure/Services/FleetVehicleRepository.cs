using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;
using Tmf.Saarthi.Infrastructure.Models.Response.FleetVehicle;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class FleetVehicleRepository : IFleetVehicleRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly IHttpService _httpService;
    private readonly ConnectionStringsOptions _connectionStringsOptions;
    private readonly InstaVeritaOptions _instaVeritaOptions;

    public FleetVehicleRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, IHttpService httpService, IOptions<InstaVeritaOptions> instaVeritaOptions)
    {
        _sqlUtility = sqlUtility;
        _httpService = httpService;
        _connectionStringsOptions = connectionStringsOptions.Value;
        _instaVeritaOptions = instaVeritaOptions.Value;
    }

    public async Task<FleetVehicleResponseModel> AddFleetVehicle(AddFleetVehicleRequestModel addFleetVehicleRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", addFleetVehicleRequestModel.FleetID),
            new SqlParameter("RCNo", addFleetVehicleRequestModel.RCNo),
            new SqlParameter("IsSubmitted", addFleetVehicleRequestModel.IsSubmitted),
            new SqlParameter("IsApproved", addFleetVehicleRequestModel.IsApproved),
            new SqlParameter("Reject_Reason", addFleetVehicleRequestModel.Reject_Reason),
            new SqlParameter("IsActive", addFleetVehicleRequestModel.IsActive),
            new SqlParameter("IsCallCenterApproved", addFleetVehicleRequestModel.IsCallCenterApproved),
            new SqlParameter("AgentRemark", addFleetVehicleRequestModel.AgentRemark),
            new SqlParameter("IsAdminApproved", addFleetVehicleRequestModel.IsAdminApproved),
            new SqlParameter("AdminRemark", addFleetVehicleRequestModel.AdminRemark),
            new SqlParameter("RegistrationDate", addFleetVehicleRequestModel.RegistrationDate),
            new SqlParameter("ExpiryDate", addFleetVehicleRequestModel.ExpiryDate),
            new SqlParameter("VehicleType", addFleetVehicleRequestModel.VehicleType),
            new SqlParameter("ChassisNo", addFleetVehicleRequestModel.ChassisNo),
            new SqlParameter("EngineNo", addFleetVehicleRequestModel.EngineNo),
            new SqlParameter("VehicleCompany", addFleetVehicleRequestModel.VehicleCompany),
            new SqlParameter("VehicleModel", addFleetVehicleRequestModel.VehicleModel),
            new SqlParameter("OwnerName", addFleetVehicleRequestModel.OwnerName),
            new SqlParameter("FirNumber", addFleetVehicleRequestModel.FirNumber),
            new SqlParameter("FirDate", addFleetVehicleRequestModel.FirDate),
            new SqlParameter("IsBlacklisted", addFleetVehicleRequestModel.IsBlacklisted),
            new SqlParameter("BlackListedReason", addFleetVehicleRequestModel.BlackListedReason),
            new SqlParameter("CreatedBy", addFleetVehicleRequestModel.CreatedBy),
            new SqlParameter("CreatedDate", addFleetVehicleRequestModel.CreatedDate),
            new SqlParameter("Comment", addFleetVehicleRequestModel.Comment)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_addFleetVehicle", parameters);

        FleetVehicleResponseModel fleetVehicleResponseModel = new FleetVehicleResponseModel();
        if (dt.Rows.Count > 0)
        {
            fleetVehicleResponseModel.VehicleID = Convert.ToInt64(dt.Rows[0]["VehicleID"]);
            fleetVehicleResponseModel.FleetID = addFleetVehicleRequestModel.FleetID;
            fleetVehicleResponseModel.RCNo = addFleetVehicleRequestModel.RCNo;
            fleetVehicleResponseModel.ErrorMessage = dt.Rows[0]["ErrorMessage"] != DBNull.Value ? Convert.ToString(dt.Rows[0]["ErrorMessage"]) : string.Empty;
        }
        return fleetVehicleResponseModel;
    }

    public async Task<List<FleetVehicleResponseModel>> BulkAddFleetVehicle(List<AddFleetVehicleRequestModel> addFleetVehicleRequestModelList)
    {      
        DataTable dataTable = new DataTable();      
        dataTable.Columns.Add("FleetID", typeof(long));
        dataTable.Columns.Add("RCNo", typeof(string));
        dataTable.Columns.Add("IsSubmitted", typeof(bool));
        dataTable.Columns.Add("IsApproved", typeof(bool));
        dataTable.Columns.Add("Reject_Reason", typeof(string));
        dataTable.Columns.Add("IsActive", typeof(bool));
        dataTable.Columns.Add("IsCallCenterApproved", typeof(bool));
        dataTable.Columns.Add("AgentRemark", typeof(string));
        dataTable.Columns.Add("IsAdminApproved", typeof(bool));
        dataTable.Columns.Add("AdminRemark", typeof(string));
        dataTable.Columns.Add("RegistrationDate", typeof(DateTime));
        dataTable.Columns.Add("ExpiryDate", typeof(DateTime));
        dataTable.Columns.Add("VehicleType", typeof(string));
        dataTable.Columns.Add("ChassisNo", typeof(string));
        dataTable.Columns.Add("EngineNo", typeof(string));
        dataTable.Columns.Add("VehicleCompany", typeof(string));
        dataTable.Columns.Add("VehicleModel", typeof(string));
        dataTable.Columns.Add("OwnerName", typeof(string));
        dataTable.Columns.Add("FirNumber", typeof(string));
        dataTable.Columns.Add("FirDate", typeof(DateTime));
        dataTable.Columns.Add("IsBlacklisted", typeof(bool));
        dataTable.Columns.Add("BlackListedReason", typeof(string));
        dataTable.Columns.Add("CreatedBy", typeof(long));
        dataTable.Columns.Add("CreatedDate", typeof(DateTime));
        dataTable.Columns.Add("Comment", typeof(string));
        foreach (var fleetVehicle in addFleetVehicleRequestModelList)
        {
            dataTable.Rows.Add(fleetVehicle.FleetID, fleetVehicle.RCNo, fleetVehicle.IsSubmitted, fleetVehicle.IsApproved, fleetVehicle.Reject_Reason,
                fleetVehicle.IsActive, fleetVehicle.IsCallCenterApproved, fleetVehicle.AgentRemark, fleetVehicle.IsAdminApproved, fleetVehicle.AdminRemark, fleetVehicle.RegistrationDate,
                fleetVehicle.ExpiryDate, fleetVehicle.VehicleType, fleetVehicle.ChassisNo, fleetVehicle.EngineNo, fleetVehicle.VehicleCompany, fleetVehicle.VehicleModel,
                fleetVehicle.OwnerName, fleetVehicle.FirNumber, fleetVehicle.FirDate, fleetVehicle.IsBlacklisted, fleetVehicle.BlackListedReason, fleetVehicle.CreatedBy, fleetVehicle.CreatedDate, fleetVehicle.Comment);
        }

        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", addFleetVehicleRequestModelList[0].FleetID),
            new SqlParameter("@AddFleetVehicleType", dataTable)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_BulkAddFleetVehicle", parameters);

        List<FleetVehicleResponseModel> fleetVehicleResponseModelList = new List<FleetVehicleResponseModel>();
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                FleetVehicleResponseModel fleetVehicleResponseModel = new FleetVehicleResponseModel();
                fleetVehicleResponseModel.FleetID = addFleetVehicleRequestModelList[0].FleetID;
                fleetVehicleResponseModel.VehicleID = Convert.ToInt64(row["VehicleID"]);
                fleetVehicleResponseModel.RCNo = (string)(row["RCNo"]);
                fleetVehicleResponseModelList.Add(fleetVehicleResponseModel);
            }         
        }
        return fleetVehicleResponseModelList;
    }

    public async Task<FleetVehicleResponseModel> DeleteFleetVehicle(DeleteFleetVehicleRequestModel deleteFleetVehicleRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("VehicleID", deleteFleetVehicleRequestModel.VehicleID)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_deleteFleetVehicle", parameters);

        FleetVehicleResponseModel fleetVehicleResponseModel = new FleetVehicleResponseModel();
        if (dt.Rows.Count > 0)
        {
            fleetVehicleResponseModel.VehicleID = Convert.ToInt64(dt.Rows[0]["VehicleID"]);
        }
        return fleetVehicleResponseModel;

    }

    public async Task<List<FleetVehicleResponseModel>> GetFleetVehicleByFleetId(GetFleetVehicleByFleetIDRequestModel getFleetVehicleByFleetIDRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", getFleetVehicleByFleetIDRequestModel.FleetID)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFleetVehicleByFleetID", parameters);

        List<FleetVehicleResponseModel> fleetVehicleResponseModels = new List<FleetVehicleResponseModel>();

        if (dt.Rows.Count > 0)
        {
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                FleetVehicleResponseModel fleetVehicleResponseModel = new FleetVehicleResponseModel();
                fleetVehicleResponseModel.VehicleID = Convert.ToInt32(dt.Rows[i]["VehicleID"]);
                fleetVehicleResponseModel.FleetID = Convert.ToInt32(dt.Rows[i]["FleetID"]);
                fleetVehicleResponseModel.RCNo = Convert.ToString(dt.Rows[i]["RCNo"]);

                fleetVehicleResponseModels.Add(fleetVehicleResponseModel);
            }
        }

        return fleetVehicleResponseModels;
    }

    public async Task<FleetVehicleResponseModel> GetFleetVehicleById(GetFleetVehicleRequestModel getFleetVehicleRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("VehicleID", getFleetVehicleRequestModel.VehicleID)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFleetVehicleById", parameters);

        FleetVehicleResponseModel fleetVehicleResponseModel = new FleetVehicleResponseModel();

        if (dt.Rows.Count > 0)
        {
            fleetVehicleResponseModel.VehicleID = Convert.ToInt32(dt.Rows[0]["VehicleID"]);
            fleetVehicleResponseModel.FleetID = Convert.ToInt32(dt.Rows[0]["FleetID"]);
            fleetVehicleResponseModel.RCNo = Convert.ToString(dt.Rows[0]["RCNo"]);
        }

        return fleetVehicleResponseModel;
    }

    public async Task<UpdateFleetVehicleRCResponseModel> UpdateFleetVehicle(UpdateFleetVehicleRCRequestModel updateFleetVehicleRCRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("VehicleID", updateFleetVehicleRCRequestModel.VehicleID),
            new SqlParameter("RCNo", updateFleetVehicleRCRequestModel.RCNo),
            new SqlParameter("IsSubmitted", updateFleetVehicleRCRequestModel.IsSubmitted),
            new SqlParameter("UpdatedBy", updateFleetVehicleRCRequestModel.UpdatedBy),
            new SqlParameter("UpdatedDate", updateFleetVehicleRCRequestModel.UpdatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateFleetVehicleRCNo", parameters);

        UpdateFleetVehicleRCResponseModel updateFleetVehicleRCResponseModel = new UpdateFleetVehicleRCResponseModel();
        if (dt.Rows.Count > 0)
        {
            updateFleetVehicleRCResponseModel.VehicleID = Convert.ToInt64(dt.Rows[0]["VehicleID"]);
            updateFleetVehicleRCResponseModel.Message = Convert.ToString(dt.Rows[0]["Message"]);
            updateFleetVehicleRCResponseModel.RCNo = updateFleetVehicleRCRequestModel.RCNo;
        }
        return updateFleetVehicleRCResponseModel;
    }

    public async Task<InstaVeritaResponseModel> GetInstaVeritaDetails(string RCNo)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("BpNo", "1");
        headers.Add("UserType", "User");

        JsonDocument result = await _httpService.GetAsync(_instaVeritaOptions.BaseUrl + _instaVeritaOptions.GetVehicleDetail + RCNo, headers);

        return JsonSerializer.Deserialize<InstaVeritaResponseModel>(result, jsonSerializerOptions) ?? throw new ArgumentNullException();
    }

    public async Task<List<VerifyFleetVehicleResponseModel>> BulkUpdateFleetDetails(List<VerifyFleetVehicleResponseModel> verifyFleetVehicleResponseModels)
    {
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("VehicleID", typeof(long));
        dataTable.Columns.Add("FleetID", typeof(long));
        dataTable.Columns.Add("RCNo", typeof(string));
        dataTable.Columns.Add("IsSubmitted", typeof(bool));
        dataTable.Columns.Add("IsApproved", typeof(bool));
        dataTable.Columns.Add("Reject_Reason", typeof(string));
        dataTable.Columns.Add("IsActive", typeof(bool));
        dataTable.Columns.Add("IsCallCenterApproved", typeof(bool));
        dataTable.Columns.Add("AgentRemark", typeof(string));
        dataTable.Columns.Add("IsAdminApproved", typeof(bool));
        dataTable.Columns.Add("AdminRemark", typeof(string));
        dataTable.Columns.Add("RegistrationDate", typeof(DateTime));
        dataTable.Columns.Add("ExpiryDate", typeof(DateTime));
        dataTable.Columns.Add("VehicleType", typeof(string));
        dataTable.Columns.Add("ChassisNo", typeof(string));
        dataTable.Columns.Add("EngineNo", typeof(string));
        dataTable.Columns.Add("VehicleCompany", typeof(string));
        dataTable.Columns.Add("VehicleModel", typeof(string));
        dataTable.Columns.Add("OwnerName", typeof(string));
        dataTable.Columns.Add("FirNumber", typeof(string));
        dataTable.Columns.Add("FirDate", typeof(DateTime));
        dataTable.Columns.Add("IsBlacklisted", typeof(bool));
        dataTable.Columns.Add("BlackListedReason", typeof(string));
        dataTable.Columns.Add("UpdatedBy", typeof(long));
        dataTable.Columns.Add("UpdatedDate", typeof(DateTime));
        foreach (var fleetVehicle in verifyFleetVehicleResponseModels)
        {
            dataTable.Rows.Add(fleetVehicle.VehicleID, fleetVehicle.FleetID , fleetVehicle.RCNo, fleetVehicle.IsSubmitted, fleetVehicle.IsApproved , fleetVehicle.Reject_Reason, 
                fleetVehicle.IsActive, fleetVehicle.IsCallCenterApproved, fleetVehicle.AgentRemark, fleetVehicle.IsAdminApproved, fleetVehicle.AdminRemark, fleetVehicle.RegistrationDate,
                fleetVehicle.ExpiryDate, fleetVehicle.VehicleType, fleetVehicle.ChassisNo, fleetVehicle.EngineNo, fleetVehicle.VehicleCompany, fleetVehicle.VehicleModel, 
                fleetVehicle.OwnerName, fleetVehicle.FirNumber, fleetVehicle.FirDate, fleetVehicle.IsBlacklisted, fleetVehicle.BlackListedReason, fleetVehicle.UpdatedBy, fleetVehicle.UpdatedDate );
        }

        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("ParFleetVehicleType", dataTable)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_bulkUpdateFleetVehicle", parameters);

        if (dt.Rows.Count == 0)
        {
            verifyFleetVehicleResponseModels = new List<VerifyFleetVehicleResponseModel>();
        }
        return verifyFleetVehicleResponseModels;
    }

    public async Task<DeactivateFleetVehicleResponseModel> DeactivateFleetVehicle(DeactivateFleetVehicleRequestModel deactivateFleetVehicleRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("VehicleID", deactivateFleetVehicleRequestModel.VehicleID),
            new SqlParameter("UpdatedBy", deactivateFleetVehicleRequestModel.UpdatedBy),
            new SqlParameter("UpdatedDate", deactivateFleetVehicleRequestModel.UpdatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_deactivateFleetVehicle", parameters);

        DeactivateFleetVehicleResponseModel deactivateFleetVehicleResponseModel = new DeactivateFleetVehicleResponseModel();
        if (dt.Rows.Count > 0)
        {
            deactivateFleetVehicleResponseModel.VehicleID = Convert.ToInt64(dt.Rows[0]["VehicleID"]);
        }
        return deactivateFleetVehicleResponseModel;
    }

    public async Task<InstaVeritaLogResponseModel> InsertInstaVeritaDetails(InstaVeritaLogRequestModel instaVeritaLogRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("VehicleID", instaVeritaLogRequestModel.VehicleID),
            new SqlParameter("Blacklisted", instaVeritaLogRequestModel.Blacklisted),
            new SqlParameter("BlacklistedReason", instaVeritaLogRequestModel.BlacklistedReason),
            new SqlParameter("ChassisNumber", instaVeritaLogRequestModel.ChassisNumber),
            new SqlParameter("EngineNumber", instaVeritaLogRequestModel.EngineNumber),
            new SqlParameter("ExpiryDate", instaVeritaLogRequestModel.ExpiryDate),
            new SqlParameter("FitnessCertificateExpiryDate", instaVeritaLogRequestModel.FitnessCertificateExpiryDate),
            new SqlParameter("FinancingAuthority", instaVeritaLogRequestModel.FinancingAuthority),
            new SqlParameter("FuelType", instaVeritaLogRequestModel.FuelType),
            new SqlParameter("MVTaxPaidUpto", instaVeritaLogRequestModel.MVTaxPaidUpto),
            new SqlParameter("MVTaxStructure", instaVeritaLogRequestModel.MVTaxStructure),
            new SqlParameter("OwnersName", instaVeritaLogRequestModel.OwnersName),
            new SqlParameter("OwnerSerialNumber", instaVeritaLogRequestModel.OwnerSerialNumber),
            new SqlParameter("PuccUpto", instaVeritaLogRequestModel.PuccUpto),
            new SqlParameter("RegistrationNumber", instaVeritaLogRequestModel.RegistrationNumber),
            new SqlParameter("RegistrationDate", instaVeritaLogRequestModel.RegistrationDate),
            new SqlParameter("RegisteringAuthority", instaVeritaLogRequestModel.RegisteringAuthority),
            new SqlParameter("RegistrationState", instaVeritaLogRequestModel.RegistrationState),
            new SqlParameter("VehicleCompany", instaVeritaLogRequestModel.VehicleCompany),
            new SqlParameter("VehicleModel", instaVeritaLogRequestModel.VehicleModel),
            new SqlParameter("VehicleType", instaVeritaLogRequestModel.VehicleType),
            new SqlParameter("VehicleAge", instaVeritaLogRequestModel.VehicleAge),
            new SqlParameter("CreatedBy", instaVeritaLogRequestModel.CreatedBy),
            new SqlParameter("CreatedDate", instaVeritaLogRequestModel.CreatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_addInstaVeritaLog", parameters);

        InstaVeritaLogResponseModel instaVeritaLogResponseModel = new InstaVeritaLogResponseModel();
        if (dt.Rows.Count > 0)
        {
            instaVeritaLogResponseModel.Log_Id = Convert.ToInt64(dt.Rows[0]["Log_Id"]);
            instaVeritaLogResponseModel.VehicleID = instaVeritaLogRequestModel.VehicleID;
            instaVeritaLogResponseModel.Blacklisted = instaVeritaLogRequestModel.Blacklisted;
            instaVeritaLogResponseModel.BlacklistedReason = instaVeritaLogRequestModel.BlacklistedReason;
            instaVeritaLogResponseModel.ChassisNumber = instaVeritaLogRequestModel.ChassisNumber;
            instaVeritaLogResponseModel.EngineNumber = instaVeritaLogRequestModel.EngineNumber;
            instaVeritaLogResponseModel.ExpiryDate = instaVeritaLogRequestModel.ExpiryDate;
            instaVeritaLogResponseModel.FitnessCertificateExpiryDate = instaVeritaLogRequestModel.FitnessCertificateExpiryDate;
            instaVeritaLogResponseModel.FinancingAuthority = instaVeritaLogRequestModel.FinancingAuthority;
            instaVeritaLogResponseModel.FuelType = instaVeritaLogRequestModel.FuelType;
            instaVeritaLogResponseModel.MVTaxPaidUpto = instaVeritaLogRequestModel.MVTaxPaidUpto;
            instaVeritaLogResponseModel.MVTaxStructure = instaVeritaLogRequestModel.MVTaxStructure;
            instaVeritaLogResponseModel.OwnersName = instaVeritaLogRequestModel.OwnersName;
            instaVeritaLogResponseModel.OwnerSerialNumber = instaVeritaLogRequestModel.OwnerSerialNumber;
            instaVeritaLogResponseModel.PuccUpto = instaVeritaLogRequestModel.PuccUpto;
            instaVeritaLogResponseModel.RegistrationNumber = instaVeritaLogRequestModel.RegistrationNumber;
            instaVeritaLogResponseModel.RegistrationDate = instaVeritaLogRequestModel.RegistrationDate;
            instaVeritaLogResponseModel.RegisteringAuthority = instaVeritaLogRequestModel.RegisteringAuthority;
            instaVeritaLogResponseModel.RegistrationState = instaVeritaLogRequestModel.RegistrationState;
            instaVeritaLogResponseModel.VehicleCompany = instaVeritaLogRequestModel.VehicleCompany;
            instaVeritaLogResponseModel.VehicleModel = instaVeritaLogRequestModel.VehicleModel;
            instaVeritaLogResponseModel.VehicleType = instaVeritaLogRequestModel.VehicleType;
            instaVeritaLogResponseModel.VehicleAge = instaVeritaLogRequestModel.VehicleAge;
            instaVeritaLogResponseModel.CreatedBy = instaVeritaLogRequestModel.CreatedBy;
            instaVeritaLogResponseModel.CreatedDate = instaVeritaLogRequestModel.CreatedDate;
        }
        return instaVeritaLogResponseModel;
    }

    public async Task<BlackListedDetailsResponseModel> InsertInstaVeritaBlackListedDetails(BlackListedDetailsRequestModel blackListedDetailsRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("InstaLogId", blackListedDetailsRequestModel.InstaLogId),
            new SqlParameter("RegistrationState", blackListedDetailsRequestModel.RegistrationState),
            new SqlParameter("RegisteringAuthority", blackListedDetailsRequestModel.RegisteringAuthority),
            new SqlParameter("RcNumber", blackListedDetailsRequestModel.RcNumber),
            new SqlParameter("FirNumber", blackListedDetailsRequestModel.FirNumber),
            new SqlParameter("FirDate", blackListedDetailsRequestModel.FirDate),
            new SqlParameter("BlacklistedDate", blackListedDetailsRequestModel.BlacklistedDate),
            new SqlParameter("BlacklistedReason", blackListedDetailsRequestModel.BlacklistedReason),
            new SqlParameter("CreatedBy", blackListedDetailsRequestModel.CreatedBy),
            new SqlParameter("CreatedDate", blackListedDetailsRequestModel.CreatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_addBlackListedDetailsLog", parameters);

        BlackListedDetailsResponseModel blackListedDetailsResponseModel = new BlackListedDetailsResponseModel();
        if (dt.Rows.Count > 0)
        {
            blackListedDetailsResponseModel.BlackListedId = Convert.ToInt64(dt.Rows[0]["BlackListedId"]);
            blackListedDetailsResponseModel.InstaLogId = blackListedDetailsRequestModel.InstaLogId;
            blackListedDetailsResponseModel.RegistrationState = blackListedDetailsRequestModel.RegistrationState;
            blackListedDetailsResponseModel.RegisteringAuthority = blackListedDetailsRequestModel.RegisteringAuthority;
            blackListedDetailsResponseModel.RcNumber = blackListedDetailsRequestModel.RcNumber;
            blackListedDetailsResponseModel.FirNumber = blackListedDetailsRequestModel.FirNumber;
            blackListedDetailsResponseModel.FirDate = blackListedDetailsRequestModel.FirDate;
            blackListedDetailsResponseModel.BlacklistedDate = blackListedDetailsRequestModel.BlacklistedDate;
            blackListedDetailsResponseModel.BlacklistedReason = blackListedDetailsRequestModel.BlacklistedReason;
            blackListedDetailsResponseModel.CreatedBy = blackListedDetailsRequestModel.CreatedBy;
            blackListedDetailsResponseModel.CreatedDate = blackListedDetailsRequestModel.CreatedDate;
        }
        return blackListedDetailsResponseModel;
    }

    public async Task<long> DeleteAllFleetVehicleByFleetId(long fleetId)
    {
        long fleetIdResponse = 0;
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", fleetId)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_DeleteAllFleetVehicleByFleetId", parameters);

        FleetVehicleResponseModel fleetVehicleResponseModel = new FleetVehicleResponseModel();
        if (dt.Rows.Count > 0)
        {
            fleetIdResponse = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }
        return fleetIdResponse;
    }

    public async Task<InstaVeritaResponseModel> GetInstaVeritaDetailsByRC(string RCNo)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("RCNo", RCNo)
        };

        DataSet ds = await _sqlUtility.ExecuteMultipleCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getInstaVeritaDetailByRCNo", parameters);

        InstaVeritaResponseModel instaVeritaResponseModel = new InstaVeritaResponseModel(); 
        instaVeritaResponseModel.BlacklistedDetails = new List<BlackListDetailsModel>();

        if (ds.Tables[0].Rows.Count > 0)
        {
            instaVeritaResponseModel.Blacklisted = ds.Tables[0].Rows[0]["blacklisted"] != DBNull.Value ? (bool)ds.Tables[0].Rows[0]["blacklisted"] : "";
            instaVeritaResponseModel.BlacklistedReason = ds.Tables[0].Rows[0]["blacklisted_reason"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["blacklisted_reason"] : "";
            instaVeritaResponseModel.ChassisNumber = ds.Tables[0].Rows[0]["chassis_number"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["chassis_number"] : "";
            instaVeritaResponseModel.EngineNumber = ds.Tables[0].Rows[0]["engine_number"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["engine_number"] : "";
            instaVeritaResponseModel.ExpiryDate = ds.Tables[0].Rows[0]["expiry_date"] != DBNull.Value ? (DateTime)ds.Tables[0].Rows[0]["expiry_date"] : null;
            instaVeritaResponseModel.FitnessCertificateExpiryDate = ds.Tables[0].Rows[0]["fitness_certificate_expiry_date"] != DBNull.Value ? (DateTime)ds.Tables[0].Rows[0]["fitness_certificate_expiry_date"] : "";
            instaVeritaResponseModel.FinancingAuthority = ds.Tables[0].Rows[0]["financing_authority"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["financing_authority"] : "";
            instaVeritaResponseModel.FuelType = ds.Tables[0].Rows[0]["fuel_type"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["fuel_type"] : "";
            instaVeritaResponseModel.MVTaxPaidUpto = ds.Tables[0].Rows[0]["mv_tax_paid_upto"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["mv_tax_paid_upto"] : "";
            instaVeritaResponseModel.MVTaxStructure = ds.Tables[0].Rows[0]["mv_tax_structure"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["mv_tax_structure"] : "";
            instaVeritaResponseModel.OwnersName = ds.Tables[0].Rows[0]["owners_name"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["owners_name"] : "";
            instaVeritaResponseModel.OwnerSerialNumber = ds.Tables[0].Rows[0]["owner_serial_number"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["owner_serial_number"] : "";
            instaVeritaResponseModel.PuccUpto = ds.Tables[0].Rows[0]["pucc_upto"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["pucc_upto"] : "";
            instaVeritaResponseModel.RegistrationNumber = ds.Tables[0].Rows[0]["registration_number"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["registration_number"] : "";
            instaVeritaResponseModel.RegistrationDate = ds.Tables[0].Rows[0]["registration_date"] != DBNull.Value ? (DateTime)ds.Tables[0].Rows[0]["registration_date"] : "";
            instaVeritaResponseModel.RegisteringAuthority = ds.Tables[0].Rows[0]["registering_authority"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["registering_authority"] : "";
            instaVeritaResponseModel.RegistrationState = ds.Tables[0].Rows[0]["registration_state"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["registration_state"] : "";
            instaVeritaResponseModel.VehicleCompany = ds.Tables[0].Rows[0]["vehicle_company"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["vehicle_company"] : "";
            instaVeritaResponseModel.VehicleModel = ds.Tables[0].Rows[0]["vehicle_model"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["vehicle_model"] : "";
            instaVeritaResponseModel.VehicleType = ds.Tables[0].Rows[0]["vehicle_type"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["vehicle_type"] : "";
            instaVeritaResponseModel.VehicleAge = ds.Tables[0].Rows[0]["vehicle_age"] != DBNull.Value ? (string)ds.Tables[0].Rows[0]["vehicle_age"] : "";
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            for(int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                BlackListDetailsModel blackListDetailsModel = new BlackListDetailsModel();
                blackListDetailsModel.RegistrationState = ds.Tables[1].Rows[i]["registration_state"] != DBNull.Value ? (string)ds.Tables[1].Rows[i]["registration_state"] : string.Empty;
                blackListDetailsModel.RegisteringAuthority = ds.Tables[1].Rows[i]["registering_authority"] != DBNull.Value ? (string)ds.Tables[1].Rows[i]["registering_authority"] : string.Empty;
                blackListDetailsModel.RcNumber = ds.Tables[1].Rows[i]["rc_number"] != DBNull.Value ? (string)ds.Tables[1].Rows[i]["rc_number"] : string.Empty;
                blackListDetailsModel.FirNumber = ds.Tables[1].Rows[i]["fir_number"] != DBNull.Value ? (string)ds.Tables[1].Rows[i]["fir_number"] : string.Empty;
                blackListDetailsModel.FirDate = ds.Tables[1].Rows[i]["fir_date"] != DBNull.Value ? (DateTime)ds.Tables[1].Rows[i]["fir_date"] : string.Empty;
                blackListDetailsModel.BlacklistedReason = ds.Tables[1].Rows[i]["blacklisted_reason"] != DBNull.Value ? (string)ds.Tables[1].Rows[i]["blacklisted_reason"] : string.Empty;
                blackListDetailsModel.BlacklistedDate = ds.Tables[1].Rows[i]["blacklisted_date"] != DBNull.Value ? (DateTime)ds.Tables[1].Rows[i]["blacklisted_date"] : string.Empty;
                instaVeritaResponseModel.BlacklistedDetails.Add(blackListDetailsModel);
            }
        }

        return instaVeritaResponseModel;
    }
}
