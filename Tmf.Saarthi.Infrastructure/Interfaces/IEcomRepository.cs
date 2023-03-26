
using Tmf.Saarthi.Core.ResponseModels.Ecom;
using Tmf.Saarthi.Infrastructure.Models.Request.Ecom;

namespace Tmf.Saarthi.Infrastructure.Interfaces
{
    public interface IEcomRepository
    {
        Task<List<GenerateManifestResponse>> GenerateManifest(EcomGenerateManifestModel ecomGenerateManifestModel);
        Task<PushShipmentTrackResponse> PushShipmentTrack(EcomPushShipmentTrackModel ecomPushShipmentTrackModel);
    }
}
