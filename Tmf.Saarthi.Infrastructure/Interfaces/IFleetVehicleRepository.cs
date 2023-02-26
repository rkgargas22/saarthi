using Tmf.Saarthi.Infrastructure.Models.Request.FleetVehicle;
using Tmf.Saarthi.Infrastructure.Models.Response.FleetVehicle;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IFleetVehicleRepository
{
    Task<FleetVehicleResponseModel> GetFleetVehicleById(GetFleetVehicleRequestModel getFleetVehicleRequestModel);

    Task<List<FleetVehicleResponseModel>> GetFleetVehicleByFleetId(GetFleetVehicleByFleetIDRequestModel getFleetVehicleByFleetIdRequestModel);

    Task<FleetVehicleResponseModel> AddFleetVehicle(AddFleetVehicleRequestModel addFleetVehicleRequestModel);
    Task<List<FleetVehicleResponseModel>> BulkAddFleetVehicle(List<AddFleetVehicleRequestModel> addFleetVehicleRequestModelList);

    Task<FleetVehicleResponseModel> DeleteFleetVehicle(DeleteFleetVehicleRequestModel deleteFleetVehicleRequestModel);
    Task<long> DeleteAllFleetVehicleByFleetId(long fleetId);

    Task<UpdateFleetVehicleRCResponseModel> UpdateFleetVehicle(UpdateFleetVehicleRCRequestModel updateFleetVehicleRCRequestModel);

    Task<InstaVeritaResponseModel> GetInstaVeritaDetails(string RCNo);

    Task<List<VerifyFleetVehicleResponseModel>> BulkUpdateFleetDetails(List<VerifyFleetVehicleResponseModel> verifyFleetVehicleResponseModels);

    Task<DeactivateFleetVehicleResponseModel> DeactivateFleetVehicle(DeactivateFleetVehicleRequestModel deactivateFleetVehicleRequestModel);

    Task<InstaVeritaLogResponseModel> InsertInstaVeritaDetails(InstaVeritaLogRequestModel instaVeritaLogRequestModel);

    Task<BlackListedDetailsResponseModel> InsertInstaVeritaBlackListedDetails(BlackListedDetailsRequestModel blackListedDetailsRequestModel);

    Task<InstaVeritaResponseModel> GetInstaVeritaDetailsByRC(string RCNo);
}
