using System.Data;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Infrastructure.Models.Request;
using Tmf.Saarthi.Infrastructure.Models.Request.Fleet;
using Tmf.Saarthi.Infrastructure.Models.Response.Fleet;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IFleetRepository
{
    Task<FleetResponseModel> GetFleet(GetFleetRequestModel getFleetRequest);

    Task<FleetResponseModel> AddFleet(AddFleetRequestModel addFleetRequest);

    Task<VerifyFleetResponseModel> GetFleetDetailByFleetId(long fleetId);

    Task<ProvisionApprovalResponseModel> ProvisionApproval(ProvisionApprovalRequestModel provisionApprovalRequest);

    Task<SanctionApprovalResponseModel> SanctionApproval(SanctionApprovalRequestModel sanctionApprovalRequestModel);

    Task<EAgreementApprovalResponseModel> EAgreementApproval(EAgreementApprovalRequestModel egreementApprovalRequest);

    Task<UpdateFleetAmountResponseModel> UpdateFleetAmount(UpdateFleetAmountRequestModel updateFleetAmountRequest);

    Task<UpdateFleetFanNoResponseModel> UpdateFleetFanNo(UpdateFleetFanNoRequestModel updateFleetFanNoRequestModel);

    Task<LetterMasterDataResponseModel> LetterMasterData(long FleetID);

    Task<CommentResponseModel> AddComment(CommentRequestModel commentRequestModel);

    Task<AdditionalInformationResponseModel> AddAdditionalInformation(AdditionalInformationRequestModel additionalInformationRequestModel);

    Task<AddressChangeResponseModel> AddressChange(AddressChangeRequestModel addressChangeRequest);

    Task<string> GetVehicleType(string VehicleModel);

    Task<List<GetDepartmentListResponseModel>> GetDepartmentLists(GetDepartmentListRequestModel getDepartmentListRequestModel);

    Task<List<GetAdditionalInfoResponseModel>> GetAdditionalInfos(GetAdditionalInfoRequestModel getAdditionalInfoRequestModel);

    Task<AddDeviationStageResponseModel> SaveDeviationStageInFleet(AddDeviationStageRequestModel addDeviationStageRequestModel);
}
