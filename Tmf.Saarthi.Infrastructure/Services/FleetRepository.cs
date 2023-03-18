using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Fleet;
using Tmf.Saarthi.Infrastructure.Models.Response.Fleet;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class FleetRepository : IFleetRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;
    
    public FleetRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
    {
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<FleetResponseModel> AddFleet(AddFleetRequestModel addFleetRequest)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("BPNumber", addFleetRequest.BPNumber),
            new SqlParameter("FanNo", addFleetRequest.FanNo),
            new SqlParameter("VehicleLimit", addFleetRequest.VehicleLimit),
            new SqlParameter("CategoryType", addFleetRequest.CategoryType),
            new SqlParameter("SubCategoryType", addFleetRequest.SubCategoryType),
            new SqlParameter("VehicleAgeCriteria", addFleetRequest.VehicleAgeCriteria),
            new SqlParameter("StampDuty", addFleetRequest.StampDuty),
            new SqlParameter("ProcessingFee", addFleetRequest.ProcessingFee),
            new SqlParameter("PerVehicleSanction", addFleetRequest.PerVehicleSanction),
            new SqlParameter("IsAgreementLetterApproved", addFleetRequest.IsAgreementLetterApproved),
            new SqlParameter("IsENachApproved", addFleetRequest.IsENachApproved),
            new SqlParameter("IsMNachApproved", addFleetRequest.IsMNachApproved),
            new SqlParameter("IsProvisionLetterApproved", addFleetRequest.IsProvisionLetterApproved),
            new SqlParameter("IsSanctionLetterApproved", addFleetRequest.IsSanctionLetterApproved),
            new SqlParameter("IsActive", addFleetRequest.IsActive),
            new SqlParameter("Comment", addFleetRequest.Comment),
            new SqlParameter("Amount", addFleetRequest.Amount),
            new SqlParameter("IRR", addFleetRequest.IRR),
            new SqlParameter("AIR", addFleetRequest.AIR),
            new SqlParameter("CreatedBy", addFleetRequest.CreatedBy),
            new SqlParameter("CreatedDate", addFleetRequest.CreatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_addfleet", parameters);

        FleetResponseModel addFleetResponseModel = new FleetResponseModel();
        if (dt.Rows.Count > 0)
        {
            addFleetResponseModel.FleetID = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return addFleetResponseModel;
    }

    public async Task<FleetResponseModel> GetFleet(GetFleetRequestModel getFleetRequest)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("BPNumber", getFleetRequest.BPNumber)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFleetByBPNumber", parameters);

        FleetResponseModel getFleetResponseModel = new FleetResponseModel();

        if (dt.Rows.Count > 0)
        {
            getFleetResponseModel.FleetID = Convert.ToInt32(dt.Rows[0]["FleetID"]);
            getFleetResponseModel.BPNumber = Convert.ToInt32(dt.Rows[0]["BPNumber"]);
            getFleetResponseModel.FanNo = Convert.ToString(dt.Rows[0]["FanNo"]);
            getFleetResponseModel.VehicleLimit = Convert.ToInt32(dt.Rows[0]["VehicleLimit"]);
            getFleetResponseModel.CategoryType = Convert.ToString(dt.Rows[0]["CategoryType"]);
            getFleetResponseModel.SubCategoryType = Convert.ToString(dt.Rows[0]["SubCategoryType"]);
            getFleetResponseModel.VehicleAgeCriteria = Convert.ToInt32(dt.Rows[0]["VehicleAgeCriteria"]);
            getFleetResponseModel.PerVehicleSanction = Convert.ToInt32(dt.Rows[0]["PerVehicleSanction"]);
            getFleetResponseModel.StampDuty = Convert.ToInt32(dt.Rows[0]["StampDuty"]);
            getFleetResponseModel.ProcessingFee = Convert.ToInt32(dt.Rows[0]["ProcessingFee"]);
            getFleetResponseModel.IsAgreementLetterApproved = dt.Rows[0]["IsAgreementLetterApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsAgreementLetterApproved"]) : null;
            getFleetResponseModel.IsENachApproved = dt.Rows[0]["IsENachApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsENachApproved"]) : null;
            getFleetResponseModel.IsMNachApproved = dt.Rows[0]["IsMNachApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsMNachApproved"]) : null;
            getFleetResponseModel.IsProvisionLetterApproved = dt.Rows[0]["IsProvisionLetterApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsProvisionLetterApproved"]) : null;
            getFleetResponseModel.IsSanctionLetterApproved = dt.Rows[0]["IsSanctionLetterApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsSanctionLetterApproved"]) : null;
            getFleetResponseModel.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
            getFleetResponseModel.Comment = Convert.ToString(dt.Rows[0]["Comment"]);
            getFleetResponseModel.Amount = dt.Rows[0]["Amount"] != DBNull.Value ? (decimal)dt.Rows[0]["Amount"] : null;
            getFleetResponseModel.IRR = dt.Rows[0]["IRR"] != DBNull.Value ? (decimal)dt.Rows[0]["IRR"] : null;
            getFleetResponseModel.AIR = dt.Rows[0]["AIR"] != DBNull.Value ? (decimal)dt.Rows[0]["AIR"] : null;
            getFleetResponseModel.LoanAmount = dt.Rows[0]["LoanAmount"] != DBNull.Value ? (decimal)dt.Rows[0]["LoanAmount"] : null;
            getFleetResponseModel.ProcessingFeeAmount = dt.Rows[0]["ProcessingFeeAmount"] != DBNull.Value ? (decimal)dt.Rows[0]["ProcessingFeeAmount"] : null;
            getFleetResponseModel.StampDutyAmount = dt.Rows[0]["StampDutyAmount"] != DBNull.Value ? (decimal)dt.Rows[0]["StampDutyAmount"] : null;
            getFleetResponseModel.DepartmentType = dt.Rows[0]["DepartmentType"] != DBNull.Value ? (string)dt.Rows[0]["DepartmentType"] : null;
            getFleetResponseModel.AdditionalInformation = dt.Rows[0]["AdditionalInformation"] != DBNull.Value ? (string)dt.Rows[0]["AdditionalInformation"] : null;
            getFleetResponseModel.AgreementDate = dt.Rows[0]["AgreementDate"] != DBNull.Value ? (DateTime)dt.Rows[0]["AgreementDate"] : null;
            getFleetResponseModel.RequestedIRR = dt.Rows[0]["RequestedIRR"] != DBNull.Value ? (decimal)dt.Rows[0]["RequestedIRR"] : null;
            getFleetResponseModel.RequestedProcessingFees = dt.Rows[0]["RequestedProcessingFees"] != DBNull.Value ? (decimal)dt.Rows[0]["RequestedProcessingFees"] : null;
            getFleetResponseModel.NewIRR = dt.Rows[0]["NewIRR"] != DBNull.Value ? (decimal)dt.Rows[0]["NewIRR"] : null;
            getFleetResponseModel.NewAIR = dt.Rows[0]["NewAIR"] != DBNull.Value ? (decimal)dt.Rows[0]["NewAIR"] : null;
            getFleetResponseModel.NewProcessing = dt.Rows[0]["NewProcessing"] != DBNull.Value ? (decimal)dt.Rows[0]["NewProcessing"] : null;
            getFleetResponseModel.RequestedAIR = dt.Rows[0]["RequestedAIR"] != DBNull.Value ? (decimal)dt.Rows[0]["RequestedAIR"] : null;
            getFleetResponseModel.AssignedTo = dt.Rows[0]["AssignedTo"] != DBNull.Value ? (long)dt.Rows[0]["AssignedTo"] : null;
            getFleetResponseModel.AssignedToRoleId = dt.Rows[0]["AssignedToRoleId"] != DBNull.Value ? (long)dt.Rows[0]["AssignedToRoleId"] : null;
            getFleetResponseModel.AgentId = dt.Rows[0]["AgentId"] != DBNull.Value ? (long)dt.Rows[0]["AgentId"] : null;
            getFleetResponseModel.AdminId = dt.Rows[0]["AdminId"] != DBNull.Value ? (long)dt.Rows[0]["AdminId"] : null;
            getFleetResponseModel.CreditId = dt.Rows[0]["CreditId"] != DBNull.Value ? (long)dt.Rows[0]["CreditId"] : null;
            getFleetResponseModel.CpcFiId = dt.Rows[0]["CpcFiId"] != DBNull.Value ? (long)dt.Rows[0]["CpcFiId"] : null;
            getFleetResponseModel.CpcTlFiId = dt.Rows[0]["CpcTlFiId"] != DBNull.Value ? (long)dt.Rows[0]["CpcTlFiId"] : null;
            getFleetResponseModel.CpcFcId = dt.Rows[0]["CpcFcId"] != DBNull.Value ? (long)dt.Rows[0]["CpcFcId"] : null;
            getFleetResponseModel.CpcTlFcId = dt.Rows[0]["CpcTlFcId"] != DBNull.Value ? (long)dt.Rows[0]["CpcTlFcId"] : null;
            getFleetResponseModel.IsAddressChanged = dt.Rows[0]["IsAddressChanged"] != DBNull.Value ? (bool)dt.Rows[0]["IsAddressChanged"] : null;
            getFleetResponseModel.CreatedUserType = dt.Rows[0]["CreatedUserType"] != DBNull.Value ? (string)dt.Rows[0]["CreatedUserType"] : null;
            getFleetResponseModel.UpdatedUserType = dt.Rows[0]["UpdatedUserType"] != DBNull.Value ? (string)dt.Rows[0]["UpdatedUserType"] : null;
            getFleetResponseModel.CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"]);
            getFleetResponseModel.UpdatedBy = !string.IsNullOrEmpty(dt.Rows[0]["UpdatedBy"].ToString()) ? Convert.ToInt64(dt.Rows[0]["UpdatedBy"]) : null;
            getFleetResponseModel.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]);
            getFleetResponseModel.UpdatedDate = !string.IsNullOrEmpty(dt.Rows[0]["UpdatedDate"].ToString()) ? Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]) : null;
        }

        return getFleetResponseModel;
    }

    public async Task<VerifyFleetResponseModel> GetFleetDetailByFleetId(long fleetId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("fleetId", fleetId)
        };

        DataSet ds = await _sqlUtility.ExecuteMultipleCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getFleetByFleetID", parameters);

        VerifyFleetResponseModel verifyFleetResponseModel = new VerifyFleetResponseModel();
        verifyFleetResponseModel.FleetVehicles = new List<VerifyFleetVehicleResponseModel>();
        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                verifyFleetResponseModel.FleetID = Convert.ToInt32(dt.Rows[0]["FleetID"]);
                verifyFleetResponseModel.BPNumber = Convert.ToInt32(dt.Rows[0]["BPNumber"]);
                verifyFleetResponseModel.FanNo = Convert.ToString(dt.Rows[0]["FanNo"]);
                verifyFleetResponseModel.VehicleLimit = Convert.ToInt32(dt.Rows[0]["VehicleLimit"]);
                verifyFleetResponseModel.CategoryType = Convert.ToString(dt.Rows[0]["CategoryType"]);
                verifyFleetResponseModel.SubCategoryType = Convert.ToString(dt.Rows[0]["SubCategoryType"]);
                verifyFleetResponseModel.VehicleAgeCriteria = Convert.ToInt32(dt.Rows[0]["VehicleAgeCriteria"]);
                verifyFleetResponseModel.PerVehicleSanction = Convert.ToInt32(dt.Rows[0]["PerVehicleSanction"]);
                verifyFleetResponseModel.StampDuty = Convert.ToInt32(dt.Rows[0]["StampDuty"]);
                verifyFleetResponseModel.ProcessingFee = (decimal)dt.Rows[0]["ProcessingFee"];
                verifyFleetResponseModel.IsAgreementLetterApproved = dt.Rows[0]["IsAgreementLetterApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsAgreementLetterApproved"]) : null;
                verifyFleetResponseModel.IsENachApproved = dt.Rows[0]["IsENachApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsENachApproved"]) : null;
                verifyFleetResponseModel.IsMNachApproved = dt.Rows[0]["IsMNachApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsMNachApproved"]) : null;
                verifyFleetResponseModel.IsProvisionLetterApproved = dt.Rows[0]["IsProvisionLetterApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsProvisionLetterApproved"]) : null;
                verifyFleetResponseModel.IsSanctionLetterApproved = dt.Rows[0]["IsSanctionLetterApproved"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["IsSanctionLetterApproved"]) : null;
                verifyFleetResponseModel.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                verifyFleetResponseModel.Comment = Convert.ToString(dt.Rows[0]["Comment"]);
                verifyFleetResponseModel.Amount = dt.Rows[0]["Amount"] != DBNull.Value ? (decimal)dt.Rows[0]["Amount"] : null;
                verifyFleetResponseModel.IRR = dt.Rows[0]["IRR"] != DBNull.Value ? (decimal)dt.Rows[0]["IRR"] : null;
                verifyFleetResponseModel.AIR = dt.Rows[0]["AIR"] != DBNull.Value ? (decimal)dt.Rows[0]["AIR"] : null;
                verifyFleetResponseModel.LoanAmount = dt.Rows[0]["LoanAmount"] != DBNull.Value ? (decimal)dt.Rows[0]["LoanAmount"] : null;
                verifyFleetResponseModel.ProcessingFeeAmount = dt.Rows[0]["ProcessingFeeAmount"] != DBNull.Value ? (decimal)dt.Rows[0]["ProcessingFeeAmount"] : null;
                verifyFleetResponseModel.StampDutyAmount = dt.Rows[0]["StampDutyAmount"] != DBNull.Value ? (decimal)dt.Rows[0]["StampDutyAmount"] : null;
                verifyFleetResponseModel.DepartmentType = dt.Rows[0]["DepartmentType"] != DBNull.Value ? (string)dt.Rows[0]["DepartmentType"] : null;
                verifyFleetResponseModel.AdditionalInformation = dt.Rows[0]["AdditionalInformation"] != DBNull.Value ? (string)dt.Rows[0]["AdditionalInformation"] : null;
                verifyFleetResponseModel.AgreementDate  = dt.Rows[0]["AgreementDate"] != DBNull.Value ? (DateTime)dt.Rows[0]["AgreementDate"] : null;
                verifyFleetResponseModel.RequestedIRR = dt.Rows[0]["RequestedIRR"] != DBNull.Value ? (decimal)dt.Rows[0]["RequestedIRR"] : null;
                verifyFleetResponseModel.RequestedProcessingFees = dt.Rows[0]["RequestedProcessingFees"] != DBNull.Value ? (decimal)dt.Rows[0]["RequestedProcessingFees"] : null;
                verifyFleetResponseModel.NewIRR = dt.Rows[0]["NewIRR"] != DBNull.Value ? (decimal)dt.Rows[0]["NewIRR"] : null;
                verifyFleetResponseModel.NewAIR  = dt.Rows[0]["NewAIR"] != DBNull.Value ? (decimal)dt.Rows[0]["NewAIR"] : null;
                verifyFleetResponseModel.NewProcessing = dt.Rows[0]["NewProcessing"] != DBNull.Value ? (decimal)dt.Rows[0]["NewProcessing"] : null;
                verifyFleetResponseModel.RequestedAIR = dt.Rows[0]["RequestedAIR"] != DBNull.Value ? (decimal)dt.Rows[0]["RequestedAIR"] : null;
                verifyFleetResponseModel.AssignedTo = dt.Rows[0]["AssignedTo"] != DBNull.Value ? (long)dt.Rows[0]["AssignedTo"] : null;
                verifyFleetResponseModel.AssignedToRoleId = dt.Rows[0]["AssignedToRoleId"] != DBNull.Value ? (long)dt.Rows[0]["AssignedToRoleId"] : null;
                verifyFleetResponseModel.AgentId = dt.Rows[0]["AgentId"] != DBNull.Value ? (long)dt.Rows[0]["AgentId"] : null;
                verifyFleetResponseModel.AdminId = dt.Rows[0]["AdminId"] != DBNull.Value ? (long)dt.Rows[0]["AdminId"] : null;
                verifyFleetResponseModel.CreditId = dt.Rows[0]["CreditId"] != DBNull.Value ? (long)dt.Rows[0]["CreditId"] : null;
                verifyFleetResponseModel.CpcFiId = dt.Rows[0]["CpcFiId"] != DBNull.Value ? (long)dt.Rows[0]["CpcFiId"] : null;
                verifyFleetResponseModel.CpcTlFiId = dt.Rows[0]["CpcTlFiId"] != DBNull.Value ? (long)dt.Rows[0]["CpcTlFiId"] : null;
                verifyFleetResponseModel.CpcFcId = dt.Rows[0]["CpcFcId"] != DBNull.Value ? (long)dt.Rows[0]["CpcFcId"] : null;
                verifyFleetResponseModel.CpcTlFcId = dt.Rows[0]["CpcTlFcId"] != DBNull.Value ? (long)dt.Rows[0]["CpcTlFcId"] : null;
                verifyFleetResponseModel.IsAddressChanged = dt.Rows[0]["IsAddressChanged"] != DBNull.Value ? (bool)dt.Rows[0]["IsAddressChanged"] : null;
                verifyFleetResponseModel.CreatedUserType = dt.Rows[0]["CreatedUserType"] != DBNull.Value ? (string)dt.Rows[0]["CreatedUserType"] : null;
                verifyFleetResponseModel.UpdatedUserType = dt.Rows[0]["UpdatedUserType"] != DBNull.Value ? (string)dt.Rows[0]["UpdatedUserType"] : null;
                verifyFleetResponseModel.CreatedBy = Convert.ToInt64(dt.Rows[0]["CreatedBy"]);
                verifyFleetResponseModel.UpdatedBy = !string.IsNullOrEmpty(dt.Rows[0]["UpdatedBy"].ToString()) ? Convert.ToInt64(dt.Rows[0]["UpdatedBy"]) : null;
                verifyFleetResponseModel.CreatedDate = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]);
                verifyFleetResponseModel.UpdatedDate = !string.IsNullOrEmpty(dt.Rows[0]["UpdatedDate"].ToString()) ? Convert.ToDateTime(dt.Rows[0]["UpdatedDate"]) : null;
                verifyFleetResponseModel.AccountNumber = dt.Rows[0]["AccountNumber"] != DBNull.Value ? (string)dt.Rows[0]["AccountNumber"] : null;
                verifyFleetResponseModel.IFSCCode = dt.Rows[0]["IFSCCode"] != DBNull.Value ? (string)dt.Rows[0]["IFSCCode"] : null;
                verifyFleetResponseModel.ApplicantName = dt.Rows[0]["ApplicantName"] != DBNull.Value ? (string)dt.Rows[0]["ApplicantName"] : null;
                verifyFleetResponseModel.FleetVehicles = new List<VerifyFleetVehicleResponseModel>();
            }

            if(ds.Tables.Count == 2 && ds.Tables[1].Rows.Count > 0)
            {
                DataTable dt1 = ds.Tables[1];
                for(int i = 0; i < dt1.Rows.Count; i++)
                {
                    VerifyFleetVehicleResponseModel verifyFleetVehicleResponse = new VerifyFleetVehicleResponseModel();
                    verifyFleetVehicleResponse.FleetID = Convert.ToInt32(dt1.Rows[i]["FleetID"]);
                    verifyFleetVehicleResponse.VehicleID = Convert.ToInt32(dt1.Rows[i]["VehicleID"]);
                    verifyFleetVehicleResponse.RCNo = Convert.ToString(dt1.Rows[i]["RCNo"]);
                    verifyFleetVehicleResponse.IsSubmitted = dt1.Rows[i]["IsSubmitted"] != DBNull.Value ? Convert.ToBoolean(dt1.Rows[i]["IsSubmitted"]) : false;
                    verifyFleetVehicleResponse.IsApproved = dt1.Rows[i]["IsApproved"] != DBNull.Value ? Convert.ToBoolean(dt1.Rows[i]["IsApproved"]) : false;
                    verifyFleetVehicleResponse.Reject_Reason = Convert.ToString(dt1.Rows[i]["Reject_Reason"]);
                    verifyFleetVehicleResponse.IsActive = Convert.ToBoolean(dt1.Rows[i]["IsActive"]);
                    verifyFleetVehicleResponse.IsCallCenterApproved = dt1.Rows[i]["IsCallCenterApproved"] != DBNull.Value ? Convert.ToBoolean(dt1.Rows[i]["IsCallCenterApproved"]) : false;
                    verifyFleetVehicleResponse.AgentRemark = Convert.ToString(dt1.Rows[i]["AgentRemark"]);
                    verifyFleetVehicleResponse.IsAdminApproved = dt1.Rows[i]["IsAdminApproved"] != DBNull.Value ? Convert.ToBoolean(dt1.Rows[i]["IsAdminApproved"]) : false;
                    verifyFleetVehicleResponse.AdminRemark = Convert.ToString(dt1.Rows[i]["AdminRemark"]);
                    verifyFleetVehicleResponse.RegistrationDate = dt1.Rows[i]["RegistrationDate"] != DBNull.Value ? Convert.ToDateTime(dt1.Rows[i]["RegistrationDate"]) : null;
                    verifyFleetVehicleResponse.ExpiryDate = dt1.Rows[i]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt1.Rows[i]["ExpiryDate"]) : null;
                    verifyFleetVehicleResponse.VehicleType = Convert.ToString(dt1.Rows[i]["VehicleType"]);
                    verifyFleetVehicleResponse.ChassisNo = Convert.ToString(dt1.Rows[i]["ChassisNo"]);
                    verifyFleetVehicleResponse.EngineNo = Convert.ToString(dt1.Rows[i]["EngineNo"]);
                    verifyFleetVehicleResponse.VehicleCompany = Convert.ToString(dt1.Rows[i]["VehicleCompany"]);
                    verifyFleetVehicleResponse.VehicleModel = Convert.ToString(dt1.Rows[i]["VehicleModel"]);
                    verifyFleetVehicleResponse.OwnerName = Convert.ToString(dt1.Rows[i]["OwnerName"]);
                    verifyFleetVehicleResponse.FirNumber = Convert.ToString(dt1.Rows[i]["FirNumber"]);
                    verifyFleetVehicleResponse.FirDate = dt1.Rows[i]["FirDate"] != DBNull.Value ? Convert.ToDateTime(dt1.Rows[i]["FirDate"]) : null;
                    verifyFleetVehicleResponse.IsBlacklisted = dt1.Rows[i]["IsBlacklisted"] != DBNull.Value ? Convert.ToBoolean(dt1.Rows[i]["IsBlacklisted"]) : false;
                    verifyFleetVehicleResponse.BlackListedReason = Convert.ToString(dt1.Rows[i]["BlackListedReason"]);
                    verifyFleetVehicleResponse.Comment = Convert.ToString(dt1.Rows[i]["Comment"]);
                    verifyFleetResponseModel.FleetVehicles.Add(verifyFleetVehicleResponse);
                }
            }
        }

        return verifyFleetResponseModel;
    }

    public async Task<ProvisionApprovalResponseModel> ProvisionApproval(ProvisionApprovalRequestModel provisionApprovalRequest)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", provisionApprovalRequest.FleetID),
            new SqlParameter("IsApproved", provisionApprovalRequest.IsApproved),
            new SqlParameter("UpdatedBy", provisionApprovalRequest.UpdatedBy),
            new SqlParameter("UpdatedDate", provisionApprovalRequest.UpdatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateFleetProvisionApproval", parameters);

        ProvisionApprovalResponseModel provisionApprovalResponseModel = new ProvisionApprovalResponseModel();
        if (dt.Rows.Count > 0)
        {
            provisionApprovalResponseModel.FleetID = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return provisionApprovalResponseModel;
    }

    public async Task<SanctionApprovalResponseModel> SanctionApproval(SanctionApprovalRequestModel sanctionApprovalRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", sanctionApprovalRequestModel.FleetID),
            new SqlParameter("IsApproved", sanctionApprovalRequestModel.IsApproved),
            new SqlParameter("UpdatedBy", sanctionApprovalRequestModel.UpdatedBy),
            new SqlParameter("UpdatedDate", sanctionApprovalRequestModel.UpdatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateFleetSanctionApproval", parameters);

        SanctionApprovalResponseModel sanctionApprovalResponseModel = new SanctionApprovalResponseModel();
        if (dt.Rows.Count > 0)
        {
            sanctionApprovalResponseModel.FleetID = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return sanctionApprovalResponseModel;
    }

    public async Task<EAgreementApprovalResponseModel> EAgreementApproval(EAgreementApprovalRequestModel egreementApprovalRequest)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", egreementApprovalRequest.FleetID),
            new SqlParameter("IsApproved", egreementApprovalRequest.IsApproved),
            new SqlParameter("UpdatedBy", egreementApprovalRequest.UpdatedBy),
            new SqlParameter("UpdatedDate", egreementApprovalRequest.UpdatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateFleetEAgreementApproval", parameters);

        EAgreementApprovalResponseModel eAgreementApprovalResponseModel = new EAgreementApprovalResponseModel();
        if (dt.Rows.Count > 0)
        {
            eAgreementApprovalResponseModel.FleetID = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return eAgreementApprovalResponseModel;
    }

    public async Task<UpdateFleetAmountResponseModel> UpdateFleetAmount(UpdateFleetAmountRequestModel updateFleetAmountRequest)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", updateFleetAmountRequest.FleetID),
            new SqlParameter("Amount", updateFleetAmountRequest.Amount),
            new SqlParameter("LoanAmount", updateFleetAmountRequest.LoanAmount),
            new SqlParameter("ProcessingFeeAmount", updateFleetAmountRequest.ProcessingFeeAmount),
            new SqlParameter("StampDutyAmount", updateFleetAmountRequest.StampDutyAmount),
            new SqlParameter("UpdatedBy", updateFleetAmountRequest.UpdatedBy),
            new SqlParameter("UpdatedDate", updateFleetAmountRequest.UpdatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateFleetAmount", parameters);

        UpdateFleetAmountResponseModel updateFleetAmountResponseModel = new UpdateFleetAmountResponseModel();
        if (dt.Rows.Count > 0)
        {
            updateFleetAmountResponseModel.FleetID = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return updateFleetAmountResponseModel;
    }

    public async Task<UpdateFleetFanNoResponseModel> UpdateFleetFanNo(UpdateFleetFanNoRequestModel updateFleetFanNoRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", updateFleetFanNoRequestModel.FleetID),
            new SqlParameter("FanNo", updateFleetFanNoRequestModel.FanNo),
            new SqlParameter("UpdatedBy", updateFleetFanNoRequestModel.UpdatedBy),
            new SqlParameter("UpdatedDate", updateFleetFanNoRequestModel.UpdatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateFleetFanNo", parameters);

        UpdateFleetFanNoResponseModel updateFleetFanNoResponseModel = new UpdateFleetFanNoResponseModel();
        if (dt.Rows.Count > 0)
        {
            updateFleetFanNoResponseModel.FleetID = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return updateFleetFanNoResponseModel;
    }

    public async Task<LetterMasterDataResponseModel> LetterMasterData(long FleetID)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", FleetID)
        };

        DataSet ds = await _sqlUtility.ExecuteMultipleCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getLetterMasterDataByFleetID", parameters);

        LetterMasterDataResponseModel letterMasterDataResponseModel = new LetterMasterDataResponseModel();
        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                letterMasterDataResponseModel.FanNo = Convert.ToString(dt.Rows[0]["FanNo"]);    
                letterMasterDataResponseModel.AgreementDate = dt.Rows[0]["AgreementDate"] != DBNull.Value ? (DateTime)dt.Rows[0]["AgreementDate"] : null; 
                letterMasterDataResponseModel.BorrowerName = Convert.ToString(dt.Rows[0]["BorrowerName"]);
                letterMasterDataResponseModel.BorrowerAddressLine1 = Convert.ToString(dt.Rows[0]["BorrowerAddressLine1"]);
                letterMasterDataResponseModel.BorrowerAddressLine2 = Convert.ToString(dt.Rows[0]["BorrowerAddressLine2"]);
                letterMasterDataResponseModel.BorrowerAddressLine3 = Convert.ToString(dt.Rows[0]["BorrowerAddressLine3"]);
                letterMasterDataResponseModel.BorrowerMobileNumber = Convert.ToString(dt.Rows[0]["BorrowerMobileNumber"]);
                letterMasterDataResponseModel.BorrowerEmailID = Convert.ToString(dt.Rows[0]["BorrowerEmailID"]);
                letterMasterDataResponseModel.TotalAmountofLoan = dt.Rows[0]["TotalAmountofLoan"] != DBNull.Value ? (decimal)dt.Rows[0]["TotalAmountofLoan"] : null;
                letterMasterDataResponseModel.InterestRate = dt.Rows[0]["InterestRate"] != DBNull.Value ? (decimal)dt.Rows[0]["InterestRate"] : null;
                letterMasterDataResponseModel.AcceleratedInterest =  dt.Rows[0]["AcceleratedInterest"] != DBNull.Value ? (decimal)dt.Rows[0]["AcceleratedInterest"] : null;
                letterMasterDataResponseModel.ProcessingFees = dt.Rows[0]["ProcessingFees"] != DBNull.Value ? (decimal)dt.Rows[0]["ProcessingFees"] : null;
                letterMasterDataResponseModel.StampDuty = dt.Rows[0]["StampDuty"] != DBNull.Value ? (decimal)dt.Rows[0]["StampDuty"] : null;
            }
        }

        return letterMasterDataResponseModel;
    }

    public async Task<CommentResponseModel> AddComment(CommentRequestModel commentRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", commentRequestModel.FleetID),
            new SqlParameter("Comment", commentRequestModel.Comment),
            new SqlParameter("CreatedBy", commentRequestModel.CreatedBy),
            new SqlParameter("CreatedUserType", commentRequestModel.CreatedUserType),
            new SqlParameter("CreatedDate", commentRequestModel.CreatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "uspAddFleetComment", parameters);

        CommentResponseModel commentResponseModel = new CommentResponseModel();
        if (dt.Rows.Count > 0)
        {
            commentResponseModel.CommentId = Convert.ToInt64(dt.Rows[0]["CmtId"]);
            commentResponseModel.ErrorMessage = Convert.ToString(dt.Rows[0]["ErrorMessage"]);
            commentResponseModel.StageId = Convert.ToInt64(dt.Rows[0]["StageId"]);
        }

        return commentResponseModel;
    }

    public async Task<AdditionalInformationResponseModel> AddAdditionalInformation(AdditionalInformationRequestModel additionalInformationRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", additionalInformationRequestModel.FleetID),
            new SqlParameter("AdditionalInformation", additionalInformationRequestModel.AdditionalInformation),
            new SqlParameter("DepartmentType", additionalInformationRequestModel.DepartmentType),
            new SqlParameter("CreatedBy", additionalInformationRequestModel.CreatedBy),
            new SqlParameter("CreatedUserType", additionalInformationRequestModel.CreatedUserType),
            new SqlParameter("CreatedDate", additionalInformationRequestModel.CreatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "uspAddFleetAdditionalInformation", parameters);

        AdditionalInformationResponseModel additionalInformationResponseModel = new AdditionalInformationResponseModel();
        if (dt.Rows.Count > 0)
        {
            additionalInformationResponseModel.AdditionalInfoId = Convert.ToInt64(dt.Rows[0]["AdditionalInfoId"]);
            additionalInformationResponseModel.ErrorMessage = Convert.ToString(dt.Rows[0]["ErrorMessage"]);
            additionalInformationResponseModel.StageId = Convert.ToInt64(dt.Rows[0]["StageId"]);
        }

        return additionalInformationResponseModel;
    }

    public async Task<AddressChangeResponseModel> AddressChange(AddressChangeRequestModel addressChangeRequest)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", addressChangeRequest.FleetID),
            new SqlParameter("IsAddressChange", addressChangeRequest.IsAddressChange),
            new SqlParameter("UpdatedBy", addressChangeRequest.UpdatedBy),
            new SqlParameter("UpdatedDate", addressChangeRequest.UpdatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateFleetAddressFlag", parameters);

        AddressChangeResponseModel addressChangeResponseModel = new AddressChangeResponseModel();
        if (dt.Rows.Count > 0)
        {
            addressChangeResponseModel.FleetID = Convert.ToInt64(dt.Rows[0]["FleetID"]);
        }

        return addressChangeResponseModel;
    }

    public async Task<string> GetVehicleType(string VehicleModel)
    {
        string VehicleType = string.Empty;
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("VehicleModel", VehicleModel)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getVehicleTypeByModel", parameters);

        if (dt.Rows.Count > 0)
        {
            VehicleType = Convert.ToString(dt.Rows[0]["VehicleType"]);
        }

        return VehicleType;
    }

    public async Task<List<GetDepartmentListResponseModel>> GetDepartmentLists(GetDepartmentListRequestModel getDepartmentListRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", getDepartmentListRequestModel.FleetId),
            new SqlParameter("Role", getDepartmentListRequestModel.Role)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "uspGetDepartmentType", parameters);

        List<GetDepartmentListResponseModel> getDepartmentListResponseModels = new List<GetDepartmentListResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                GetDepartmentListResponseModel getDepartmentListResponseModel = new GetDepartmentListResponseModel();
                getDepartmentListResponseModel.DepartmentCode = (string)dt.Rows[i]["DepartmentCode"];
                getDepartmentListResponseModel.DepartmentName = (string)dt.Rows[i]["DepartmentName"];

                getDepartmentListResponseModels.Add(getDepartmentListResponseModel);
            }
        }

        return getDepartmentListResponseModels;
    }

    public async Task<List<GetAdditionalInfoResponseModel>> GetAdditionalInfos(GetAdditionalInfoRequestModel getAdditionalInfoRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", getAdditionalInfoRequestModel.FleetId),
            new SqlParameter("Department", getAdditionalInfoRequestModel.DepartmentCode)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "uspGetAddtionalInformationData", parameters);

        List<GetAdditionalInfoResponseModel> getAdditionalInfoResponseModels = new List<GetAdditionalInfoResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GetAdditionalInfoResponseModel getAdditionalInfoResponseModel = new GetAdditionalInfoResponseModel();
                getAdditionalInfoResponseModel.AdditionalInfoId = (long)dt.Rows[i]["AdditionalInfoId"];
                getAdditionalInfoResponseModel.Comment = (string)dt.Rows[i]["Comment"];
                getAdditionalInfoResponseModel.DepartmentCode = (string)dt.Rows[i]["DepartmentCode"];
                getAdditionalInfoResponseModel.RoleName = (string)dt.Rows[i]["RoleName"];
                getAdditionalInfoResponseModel.AssignedTo = (string)dt.Rows[i]["AssignedTo"];
                getAdditionalInfoResponseModel.FleetID = (long)dt.Rows[i]["FleetID"];
                getAdditionalInfoResponseModel.StageCode = (string)dt.Rows[i]["StageCode"];

                getAdditionalInfoResponseModels.Add(getAdditionalInfoResponseModel);
            }
        }

        return getAdditionalInfoResponseModels;
    }
}
