using Tmf.Saarthi.Core.RequestModels.Ecom;
using Tmf.Saarthi.Core.ResponseModels.Ecom;

namespace Tmf.Saarthi.Manager.Interfaces
{
    public interface IEcomManager
    {
        Task<List<GenerateManifestResponse>> GenerateManifest(int fleetId, AdditionalInformation ecomManifestRequest);
        //Task<List<GenerateManifestResponse>> GenerateManifest(GenerateManifestRequest generateManifestRequest);

        Task<List<RescheduleOrCancelAppointmentResponse>> RescheduleOrCancelAppointment(int fleetId, AdditionalInformation rescheduleOrCancelAppointment);

        Task<PushShipmentTrackResponse> PushShipmentTrack(PushShipmentStatusRequest pushShipmentStatus);
        //Task<PushShipmentTrackResponse> PushShipmentTrack(PushShipmentStatusRequest pushShipmentStatus);

        //Task<PullShipmentTrackResponse> PullShipmentTrack(PullShipmentStatusRequest pullShipmentStatus);
    }
}
