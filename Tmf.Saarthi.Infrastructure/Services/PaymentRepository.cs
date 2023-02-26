using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Payment;
using Tmf.Saarthi.Infrastructure.Models.Response.Payment;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class PaymentRepository : IPaymentRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly ConnectionStringsOptions _connectionStringsOptions;
    private readonly PaymentOptions _paymentOptions;
    private readonly IHttpService _httpService;
    public PaymentRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, IOptions<PaymentOptions> paymentOptions, IHttpService httpService)
    {
        _sqlUtility = sqlUtility;
        _connectionStringsOptions = connectionStringsOptions.Value;
        _paymentOptions = paymentOptions.Value;
        _httpService = httpService;
    }

    public async Task<SavePaymentStatusResponseModel> SavePaymentStatus(SavePaymentStatusRequestModel savePaymentStatusRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("Source", savePaymentStatusRequestModel.Source),
            new SqlParameter("BPNumber", savePaymentStatusRequestModel.BPNumber),
            new SqlParameter("TxnID", savePaymentStatusRequestModel.TxnID),
            new SqlParameter("Status", savePaymentStatusRequestModel.Status),
            new SqlParameter("FanNo", savePaymentStatusRequestModel.FanNo),
            new SqlParameter("TxnNo", savePaymentStatusRequestModel.TxnNo),
            new SqlParameter("TxnDateTime", savePaymentStatusRequestModel.TxnDateTime)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_add_Payment_status", parameters);

        SavePaymentStatusResponseModel savePaymentStatusResponseModel = new SavePaymentStatusResponseModel();
        if (dt.Rows.Count > 0)
        {
            savePaymentStatusResponseModel.ReqID = (long)dt.Rows[0]["ReqID"];
        }

        return savePaymentStatusResponseModel;
    }

    public async Task<GetPaymentStatusResponseModel> GetPaymentStatus(long FleetID)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", FleetID)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_get_PaymentStatusByFleetID", parameters);

        GetPaymentStatusResponseModel getPaymentStatusResponseModel = new GetPaymentStatusResponseModel();
        if (dt.Rows.Count > 0)
        {
            getPaymentStatusResponseModel.PaymentID = (long)dt.Rows[0]["PaymentID"];
            getPaymentStatusResponseModel.ReqId = (string)dt.Rows[0]["ReqId"];
            getPaymentStatusResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
            getPaymentStatusResponseModel.PayEMI = (string)dt.Rows[0]["PayEMI"];
            getPaymentStatusResponseModel.FanNo = (string)dt.Rows[0]["FanNo"];
            getPaymentStatusResponseModel.Source = (string)dt.Rows[0]["Source"];
            getPaymentStatusResponseModel.Amount = dt.Rows[0]["Amount"] != DBNull.Value ? (decimal)dt.Rows[0]["Amount"] : null;
            getPaymentStatusResponseModel.MobileNo = (string)dt.Rows[0]["MobileNo"];
            getPaymentStatusResponseModel.State = (string)dt.Rows[0]["State"];
            getPaymentStatusResponseModel.OrderId = Convert.ToString(dt.Rows[0]["OrderId"]);
            getPaymentStatusResponseModel.TransactionId = Convert.ToString(dt.Rows[0]["TransactionId"]);
            getPaymentStatusResponseModel.Status = Convert.ToString(dt.Rows[0]["Status"]);
            getPaymentStatusResponseModel.TransactionDateTime = dt.Rows[0]["TransactionDateTime"] != DBNull.Value ? (DateTime)dt.Rows[0]["TransactionDateTime"] : null;
            getPaymentStatusResponseModel.UtrNo = Convert.ToString(dt.Rows[0]["UtrNo"]);
            getPaymentStatusResponseModel.SapdocNo = Convert.ToString(dt.Rows[0]["SapdocNo"]);
            getPaymentStatusResponseModel.PostingDate = dt.Rows[0]["PostingDate"] != DBNull.Value ? (DateTime)dt.Rows[0]["PostingDate"] : null;
            getPaymentStatusResponseModel.IsActive = (bool)dt.Rows[0]["IsActive"];
            getPaymentStatusResponseModel.CreatedBy = (long)dt.Rows[0]["CreatedBy"];
            getPaymentStatusResponseModel.CreatedDate = (DateTime)dt.Rows[0]["CreatedDate"];
        }

        return getPaymentStatusResponseModel;
    }

    public async Task<GetPaymentUrlResponseModel> GetPaymentUrl(GetPaymentUrlRequestModel getPaymentUrlRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", getPaymentUrlRequestModel.FleetID),
            new SqlParameter("FanNo", getPaymentUrlRequestModel.FanNo),
            new SqlParameter("Amount", getPaymentUrlRequestModel.Amount),
            new SqlParameter("MobileNo", getPaymentUrlRequestModel.MobileNo),
            new SqlParameter("PayEMI", getPaymentUrlRequestModel.PayEMI),
            new SqlParameter("Source", getPaymentUrlRequestModel.Source),
            new SqlParameter("State", getPaymentUrlRequestModel.State),
            new SqlParameter("IsActive", getPaymentUrlRequestModel.IsActive),
            new SqlParameter("CreatedBy", getPaymentUrlRequestModel.CreatedBy),
            new SqlParameter("CreatedDate", getPaymentUrlRequestModel.CreatedDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_add_PaymentData", parameters);

        if (dt.Rows.Count > 0)
        {
            getPaymentUrlRequestModel.ReqID = (string)dt.Rows[0]["reqID"];
        }

        string Url = _paymentOptions.Url;
        Url = Url.Replace("{payemi}", getPaymentUrlRequestModel.PayEMI);
        Url = Url.Replace("{source}", getPaymentUrlRequestModel.Source);
        Url = Url.Replace("{fan}", getPaymentUrlRequestModel.FanNo);
        Url = Url.Replace("{viewfrom}", getPaymentUrlRequestModel.ViewFrom);
        Url = Url.Replace("{amount}", getPaymentUrlRequestModel.Amount.ToString());
        Url = Url.Replace("{cc}", getPaymentUrlRequestModel.CC.ToString());
        Url = Url.Replace("{bpno}", getPaymentUrlRequestModel.BPNumber.ToString());
        Url = Url.Replace("{state}", getPaymentUrlRequestModel.State);
        Url = Url.Replace("{reqId}", getPaymentUrlRequestModel.ReqID);
        Url = Url.Replace("{mobile}", getPaymentUrlRequestModel.MobileNo);

        GetPaymentUrlResponseModel getPaymentUrlResponseModel = new GetPaymentUrlResponseModel();
        getPaymentUrlResponseModel.Url = Url;

        return getPaymentUrlResponseModel;
    }

    public async Task<List<GetRazorPayStatusResponseModel>> GetRazorPayStatus(GetRazorPayStatusRequestModel getRazorPayStatusRequestModel)
    {
        var jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = true };
        Dictionary<string, string> headers = new Dictionary<string, string>();
        //headers.Add("BpNo", "1");
        //headers.Add("UserType", "User");

        JsonDocument result = await _httpService.PostAsync(_paymentOptions.StatusAPI, getRazorPayStatusRequestModel, headers);

        try {
            return JsonSerializer.Deserialize<List<GetRazorPayStatusResponseModel>>(result, jsonSerializerOptions);
        }
        catch {
            return new List<GetRazorPayStatusResponseModel>();
        }
    }

    public async Task<SaveRazorPayStatusResponseModel> SaveRazorPayStatus(SaveRazorPayStatusRequestModel saveRazorPayStatusRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetID", saveRazorPayStatusRequestModel.FleetID),
            new SqlParameter("ReqId", saveRazorPayStatusRequestModel.ReqId),
            new SqlParameter("TxnId", saveRazorPayStatusRequestModel.TxnId),
            new SqlParameter("Status", saveRazorPayStatusRequestModel.Status),
            new SqlParameter("OrderId", saveRazorPayStatusRequestModel.OrderId),
            new SqlParameter("UtrNo", saveRazorPayStatusRequestModel.UtrNo),
            new SqlParameter("SapdocNo", saveRazorPayStatusRequestModel.SapdocNo),
            new SqlParameter("PostingDate", saveRazorPayStatusRequestModel.PostingDate)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_updateRazorPayStatus", parameters);

        SaveRazorPayStatusResponseModel saveRazorPayStatusResponseModel = new SaveRazorPayStatusResponseModel();
        if (dt.Rows.Count > 0)
        {
            saveRazorPayStatusResponseModel.ReqId = (long)dt.Rows[0]["ReqId"];
        }

        return saveRazorPayStatusResponseModel;
    }
}

