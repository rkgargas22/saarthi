using Tmf.Saarthi.Core.RequestModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.Fleet;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IFleetManager
{
    Task<GetFleetResponse> GetByBPNumber(long BPNumber);

    Task<GetFleetResponse> Add(long BPNumber);

    Task<VerifyFleetResponse> Verify(long fleetId);

    Task<VerifyFleetResponse> GetFleetByFleetId(long fleetId);

    Task<ProvisionApprovalResponse> ProvisionApproval(long FleetID, ProvisionApprovalRequest provisionApprovalRequest);

    Task<SanctionApprovalResponse> SanctionApproval(long FleetID, SanctionApprovalRequest sanctionApprovalRequest);

    Task<EAgreementApprovalResponse> EAgreementApproval(long FleetID, EAgreementApprovalRequest eAgreementApprovalRequest);

    Task<UpdateFleetAmountResponse> UpdateFleetAmount(UpdateFleetAmountRequest updateFleetAmountRequest);

    Task<UpdateFleetFanNoResponse> UpdateFleetFanNo(UpdateFleetFanNoRequest updateFleetFanNoRequest);

    Task<LetterMasterDataResponse> LetterMasterData(long FleetID);

    Task<CommentResponse> UpdateComment(long FleetID, CommentRequest commentRequest);

    Task<AdditionalInformationResponse> UpdateAdditionalInformation(long FleetID, AdditionalInformationRequest additionalInformationRequest);

    Task<AddressChangeResponse> AddressChange(long FleetID, AddressChangeRequest addressChangeRequest);
}
