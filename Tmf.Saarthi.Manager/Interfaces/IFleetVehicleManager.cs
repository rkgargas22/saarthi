using Tmf.Saarthi.Core.RequestModels.FleetVehicle;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.FleetVehicle;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IFleetVehicleManager
{
    Task<GetFleetVehicleResponse> GetFleetVehicleById(long VehicleID);

    Task<List<GetFleetVehicleByFleetIDResponse>> GetFleetVehicleByFleetID(long fleetID);

    Task<AddFleetVehicleResponse> AddFleetVehicle(AddFleetVehicleRequest addFleetVehicleRequest);
    Task<BulkAddFleetVehicleResponse> BulkAddFleetVehicle(long fleetId, BulkAddFleetVehicleRequest bulkAddFleetVehicleRequest);

    Task<DeleteFleetVehicleResponse> DeleteFleetVehicle(long VehicleID);
    Task<DeleteAllFleetVehicleResponse> DeleteAllFleetVehicleByFleetId(long fleetID);

    Task<UpdateFleetVehicleRCResponse> UpdateFleetVehicleRC(long VehicleID, UpdateFleetVehicleRCRequest updateFleetVehicleRCRequest);

    Task<InstaVeritaResponse> InstaVeritaData(string RCNo);

    Task<List<VerifyFleetVehicleResponse>> BulkUpdateFleetDetails(List<VerifyFleetVehicleResponse> verifyFleetVehicleResponses);

    Task<DeactivateFleetVehicleResponse> DeactivateFleetVehicle(long VehicleID);

    Task<InstaVeritaLogResponse> InsertInstaVeritaDetails(InstaVeritaLogRequest instaVeritaLogRequest);

    Task<BlackListedDetailsResponse> InsertInstaVeritaBlackListedDetails(BlackListedDetailsRequest blackListedDetailsRequest);
}
