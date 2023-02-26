using Tmf.Saarthi.Core.RequestModels.Payment;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.Payment;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IPaymentManager
{
    Task<SavePaymentStatusResponse> SavePaymentStatus(SavePaymentStatusRequest savePaymentStatusRequest);

    Task<GetPaymentStatusResponse> GetPaymentStatus(long FleetID, string ReqType);

    Task<GetPaymentUrlResponse> GetPaymentUrl(long FleetID);

    Task<VerifyFleetResponse> GeneratePaymentDetails(long FleetID);

    Task<GetRazorPayStatusResponse> GetRazorPayStatus(GetRazorPayStatusRequest getRazorPayStatusRequest);
}
