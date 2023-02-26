using System.Diagnostics;
using Tmf.Saarthi.Core.RequestModels.DMS;
using Tmf.Saarthi.Core.RequestModels.Fleet;
using Tmf.Saarthi.Core.RequestModels.Payment;
using Tmf.Saarthi.Core.ResponseModels.DMS;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.Payment;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Payment;
using Tmf.Saarthi.Infrastructure.Models.Response.Payment;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class PaymentManager : IPaymentManager
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IFleetManager _fleetManager;
    public PaymentManager(IPaymentRepository paymentRepository, IFleetManager fleetManager)
    {
        _paymentRepository = paymentRepository;
        _fleetManager = fleetManager;
    }

    public async Task<SavePaymentStatusResponse> SavePaymentStatus(SavePaymentStatusRequest savePaymentStatusRequest)
    {
        SavePaymentStatusRequestModel savePaymentStatusRequestModel = new SavePaymentStatusRequestModel();
        savePaymentStatusRequestModel.Source = savePaymentStatusRequest.Source;
        savePaymentStatusRequestModel.BPNumber = savePaymentStatusRequest.BPNumber;
        savePaymentStatusRequestModel.TxnID = savePaymentStatusRequest.TxnID;
        savePaymentStatusRequestModel.FanNo = savePaymentStatusRequest.FanNo;
        savePaymentStatusRequestModel.TxnNo = savePaymentStatusRequest.TxnNo;
        savePaymentStatusRequestModel.Status = savePaymentStatusRequest.Status;
        savePaymentStatusRequestModel.TxnDateTime = savePaymentStatusRequest.TxnDateTime;

        SavePaymentStatusResponseModel savePaymentStatusResponseModel = await _paymentRepository.SavePaymentStatus(savePaymentStatusRequestModel);

        SavePaymentStatusResponse savePaymentStatusResponse = new SavePaymentStatusResponse();
        if(savePaymentStatusResponseModel.ReqID != 0)
        {
            savePaymentStatusResponse.Message = "Success";
        }
        else
        {
            savePaymentStatusResponse.Message = "Failure";
        }

        return savePaymentStatusResponse;
    }

    public async Task<GetPaymentStatusResponse> GetPaymentStatus(long FleetID, string ReqType)
    {
        GetPaymentStatusResponseModel getPaymentStatusResponseModel = await _paymentRepository.GetPaymentStatus(FleetID);

        if(getPaymentStatusResponseModel != null && string.IsNullOrEmpty(getPaymentStatusResponseModel.Status) && ReqType == "S") 
        {
            GetRazorPayStatusRequest getRazorPayStatusRequest = new GetRazorPayStatusRequest();
            getRazorPayStatusRequest.ReqNo = getPaymentStatusResponseModel.ReqId;
            getRazorPayStatusRequest.ReqFlag = ReqType;
            getRazorPayStatusRequest.TID = "aGZkZzY1cjQ3Mw==";
            getRazorPayStatusRequest.Token = "aGZkZ3I2NDg5NjgzNzZoZmRncg==";

            GetRazorPayStatusResponse getRazorPayStatusResponse = await GetRazorPayStatus(getRazorPayStatusRequest);

            SaveRazorPayStatusRequestModel saveRazorPayStatusRequestModel = new SaveRazorPayStatusRequestModel();
            saveRazorPayStatusRequestModel.FleetID = getPaymentStatusResponseModel.FleetID;
            saveRazorPayStatusRequestModel.ReqId = getPaymentStatusResponseModel.ReqId;
            saveRazorPayStatusRequestModel.Status = getPaymentStatusResponseModel.Status;

            SaveRazorPayStatusResponseModel saveRazorPayStatusResponseModel = await _paymentRepository.SaveRazorPayStatus(saveRazorPayStatusRequestModel);
            if (saveRazorPayStatusResponseModel.ReqId != 0)
            {
                getPaymentStatusResponseModel.Status = getRazorPayStatusResponse.Status;
            }
        }
        else if(getPaymentStatusResponseModel != null && string.IsNullOrEmpty(getPaymentStatusResponseModel.UtrNo) && ReqType == "T")
        {
            GetRazorPayStatusRequest getRazorPayStatusRequest = new GetRazorPayStatusRequest();
            getRazorPayStatusRequest.ReqNo = getPaymentStatusResponseModel.ReqId;
            getRazorPayStatusRequest.ReqFlag = ReqType;
            getRazorPayStatusRequest.TID = "aGZkZzY1cjQ3Mw==";
            getRazorPayStatusRequest.Token = "aGZkZ3I2NDg5NjgzNzZoZmRncg==";

            GetRazorPayStatusResponse getRazorPayStatusResponse = await GetRazorPayStatus(getRazorPayStatusRequest);

            SaveRazorPayStatusRequestModel saveRazorPayStatusRequestModel = new SaveRazorPayStatusRequestModel();
            saveRazorPayStatusRequestModel.FleetID = getPaymentStatusResponseModel.FleetID;
            saveRazorPayStatusRequestModel.ReqId = getPaymentStatusResponseModel.ReqId;
            saveRazorPayStatusRequestModel.Status = getPaymentStatusResponseModel.Status;
            saveRazorPayStatusRequestModel.OrderId = getPaymentStatusResponseModel.OrderId;
            saveRazorPayStatusRequestModel.SapdocNo = getPaymentStatusResponseModel.SapdocNo;
            saveRazorPayStatusRequestModel.UtrNo = getPaymentStatusResponseModel.UtrNo;
            saveRazorPayStatusRequestModel.TxnId = getPaymentStatusResponseModel.TransactionId;

            SaveRazorPayStatusResponseModel saveRazorPayStatusResponseModel = await _paymentRepository.SaveRazorPayStatus(saveRazorPayStatusRequestModel);
            if (saveRazorPayStatusResponseModel.ReqId != 0)
            {
                getPaymentStatusResponseModel.Status = getRazorPayStatusResponse.Status;
                getPaymentStatusResponseModel.SapdocNo = getRazorPayStatusResponse.SapdocNo;
                getPaymentStatusResponseModel.UtrNo = getRazorPayStatusResponse.UtrNo;
                getPaymentStatusResponseModel.TransactionId = getRazorPayStatusResponse.TxnId;
                getPaymentStatusResponseModel.PostingDate = getRazorPayStatusResponse.PostingDate;
            }
        }

        GetPaymentStatusResponse getPaymentStatusResponse = new GetPaymentStatusResponse();
        getPaymentStatusResponse.PaymentID = getPaymentStatusResponseModel.PaymentID;
        getPaymentStatusResponse.ReqId = getPaymentStatusResponseModel.ReqId;
        getPaymentStatusResponse.FleetID = getPaymentStatusResponseModel.FleetID;
        getPaymentStatusResponse.PayEMI = getPaymentStatusResponseModel.PayEMI;
        getPaymentStatusResponse.FanNo = getPaymentStatusResponseModel.FanNo;
        getPaymentStatusResponse.Source = getPaymentStatusResponseModel.Source;
        getPaymentStatusResponse.Amount = getPaymentStatusResponseModel.Amount;
        getPaymentStatusResponse.MobileNo = getPaymentStatusResponseModel.MobileNo;
        getPaymentStatusResponse.State = getPaymentStatusResponseModel.State;
        getPaymentStatusResponse.OrderId = getPaymentStatusResponseModel.OrderId;
        getPaymentStatusResponse.TransactionId = getPaymentStatusResponseModel.TransactionId;
        getPaymentStatusResponse.Status = getPaymentStatusResponseModel.Status;
        getPaymentStatusResponse.TransactionDateTime = getPaymentStatusResponseModel.TransactionDateTime;
        getPaymentStatusResponse.UtrNo = getPaymentStatusResponseModel.UtrNo;
        getPaymentStatusResponse.SapdocNo = getPaymentStatusResponseModel.SapdocNo;
        getPaymentStatusResponse.PostingDate = getPaymentStatusResponseModel.PostingDate;

        return getPaymentStatusResponse;
    }

    public async Task<GetPaymentUrlResponse> GetPaymentUrl(long FleetID)
    {
        VerifyFleetResponse verifyFleetResponse = await _fleetManager.GetFleetByFleetId(FleetID);

        GetPaymentUrlRequestModel getPaymentUrlRequestModel = new GetPaymentUrlRequestModel();
        getPaymentUrlRequestModel.PayEMI = "FanNumber";
        getPaymentUrlRequestModel.Source = "DGP";
        getPaymentUrlRequestModel.FanNo = verifyFleetResponse.FanNo;
        getPaymentUrlRequestModel.ViewFrom = "mobile";
        getPaymentUrlRequestModel.Amount = verifyFleetResponse.Amount;
        getPaymentUrlRequestModel.CC = 8000;
        getPaymentUrlRequestModel.BPNumber = verifyFleetResponse.BPNumber;
        getPaymentUrlRequestModel.State = "MH";
        getPaymentUrlRequestModel.ReqID = string.Empty;
        getPaymentUrlRequestModel.MobileNo = "7021480141";
        getPaymentUrlRequestModel.FleetID = verifyFleetResponse.FleetID;
        getPaymentUrlRequestModel.IsActive = true;
        getPaymentUrlRequestModel.CreatedBy = 41;
        getPaymentUrlRequestModel.CreatedDate = DateTime.Now;

        GetPaymentUrlResponseModel getPaymentUrlResponseModel = await _paymentRepository.GetPaymentUrl(getPaymentUrlRequestModel);
        GetPaymentUrlResponse getPaymentUrlResponse = new GetPaymentUrlResponse();
        getPaymentUrlResponse.Url = getPaymentUrlResponseModel.Url;

        return getPaymentUrlResponse;
    }

    public async Task<VerifyFleetResponse> GeneratePaymentDetails(long FleetID)
    {
        VerifyFleetResponse verifyFleetResponse = await _fleetManager.GetFleetByFleetId(FleetID);

        //if (string.IsNullOrEmpty(verifyFleetResponse.FanNo))
        //{
        //    GenerateFanNoRequest generateFanNoRequest = new GenerateFanNoRequest();
        //    generateFanNoRequest.BranchCode = "50233";
        //    generateFanNoRequest.ProcessType = "TMFD";
        //    generateFanNoRequest.SchemeName = "FUEL_LOAN";
        //    generateFanNoRequest.LoanType = "autoCV";
        //    generateFanNoRequest.ApplicantName = "testing";
        //    generateFanNoRequest.BdmName = "";
        //    generateFanNoRequest.DsaName = "";
        //    generateFanNoRequest.DealerName = "DIRECT";
        //    generateFanNoRequest.DealerCode = "DIRECT";

        //    GenerateFanNoResponse generateFanNoResponse = await _dMSManager.GenerateFanNo(generateFanNoRequest);
        //    string FanNo = generateFanNoResponse.FanNo;

        //    UpdateFleetFanNoRequest updateFleetFanNoRequest = new UpdateFleetFanNoRequest();
        //    updateFleetFanNoRequest.FleetID = FleetID;
        //    updateFleetFanNoRequest.FanNo = FanNo;
        //    UpdateFleetFanNoResponse updateFleetFanNoResponse = await _fleetManager.UpdateFleetFanNo(updateFleetFanNoRequest);

        //    if(updateFleetFanNoResponse.FleetID != 0)
        //    {
        //        verifyFleetResponse.FanNo = FanNo;
        //    }
        //}

        int VehicleCount = 0;

        foreach (var fleetVehicle in verifyFleetResponse.FleetVehicles)
        {
            if (fleetVehicle.IsApproved)
            {
                VehicleCount++;
            }
        }

        if (VehicleCount > 0)
        {
            decimal LoanAmount = verifyFleetResponse.PerVehicleSanction * VehicleCount;
            decimal ProcessingFeeAmount = LoanAmount * verifyFleetResponse.ProcessingFee;
            decimal StampDutyAmount = verifyFleetResponse.StampDuty;
            decimal Amount = ProcessingFeeAmount + StampDutyAmount;
            UpdateFleetAmountRequest updateFleetAmountRequest = new UpdateFleetAmountRequest();
            updateFleetAmountRequest.FleetID = FleetID;
            updateFleetAmountRequest.Amount = Amount;
            updateFleetAmountRequest.LoanAmount = LoanAmount;
            updateFleetAmountRequest.ProcessingFeeAmount = ProcessingFeeAmount;
            updateFleetAmountRequest.StampDutyAmount = verifyFleetResponse.StampDuty;

            UpdateFleetAmountResponse updateFleetAmountResponse = await _fleetManager.UpdateFleetAmount(updateFleetAmountRequest);
            if(updateFleetAmountResponse.FleetID != 0)
            {
                verifyFleetResponse.Amount = Amount;
                verifyFleetResponse.LoanAmount = LoanAmount;
                verifyFleetResponse.ProcessingFeeAmount = ProcessingFeeAmount;
                verifyFleetResponse.StampDutyAmount = StampDutyAmount;
            }
        }

        return verifyFleetResponse;
    }

    public async Task<GetRazorPayStatusResponse> GetRazorPayStatus(GetRazorPayStatusRequest getRazorPayStatusRequest)
    {
        GetRazorPayStatusRequestModel getRazorPayStatusRequestModel = new GetRazorPayStatusRequestModel();
        getRazorPayStatusRequestModel.ReqNo = getRazorPayStatusRequest.ReqNo;
        getRazorPayStatusRequestModel.ReqFlag = getRazorPayStatusRequest.ReqFlag;
        getRazorPayStatusRequestModel.TID = getRazorPayStatusRequest.TID;
        getRazorPayStatusRequestModel.Token = getRazorPayStatusRequest.Token;

        List<GetRazorPayStatusResponseModel> getRazorPayStatusResponseModel = await _paymentRepository.GetRazorPayStatus(getRazorPayStatusRequestModel);

        GetRazorPayStatusResponse getRazorPayStatusResponse = new GetRazorPayStatusResponse();
        if (getRazorPayStatusResponseModel.Count() > 0)
        {
            getRazorPayStatusResponse.TxnId = getRazorPayStatusResponseModel[0].TxnId;
            getRazorPayStatusResponse.Status = getRazorPayStatusResponseModel[0].Status;
            getRazorPayStatusResponse.OrderId = getRazorPayStatusResponseModel[0].OrderId;
            getRazorPayStatusResponse.UtrNo = getRazorPayStatusResponseModel[0].UtrNo;
            getRazorPayStatusResponse.SapdocNo = getRazorPayStatusResponseModel[0].SapdocNo;
            getRazorPayStatusResponse.PostingDate = !string.IsNullOrEmpty(getRazorPayStatusResponseModel[0].PostingDate) ? Convert.ToDateTime(getRazorPayStatusResponseModel[0].PostingDate) : null;
        }

        return getRazorPayStatusResponse;
    }
}
