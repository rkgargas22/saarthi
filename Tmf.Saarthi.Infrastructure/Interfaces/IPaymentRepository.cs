using Tmf.Saarthi.Infrastructure.Models.Request.Payment;
using Tmf.Saarthi.Infrastructure.Models.Response.Payment;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IPaymentRepository
{
    Task<SavePaymentStatusResponseModel> SavePaymentStatus(SavePaymentStatusRequestModel savePaymentStatusRequestModel);

    Task<GetPaymentStatusResponseModel> GetPaymentStatus(long FleetID);

    Task<GetPaymentUrlResponseModel> GetPaymentUrl(GetPaymentUrlRequestModel getPaymentUrlRequestModel);

    Task<List<GetRazorPayStatusResponseModel>> GetRazorPayStatus(GetRazorPayStatusRequestModel getRazorPayStatusRequestModel);

    Task<SaveRazorPayStatusResponseModel> SaveRazorPayStatus(SaveRazorPayStatusRequestModel saveRazorPayStatusRequestModel);
}
