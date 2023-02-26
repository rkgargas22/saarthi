using Tmf.Saarthi.Core.RequestModels.Ocr;
using Tmf.Saarthi.Core.ResponseModels.Ocr;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IOcrManager
{
    Task<AddressDetailsResponse> GetAddressDetails(AddressDetailsRequest getAddressDetailsRequest);
}
