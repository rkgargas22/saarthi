using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.ProvisionalLette;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.ProvisionalLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.ProvisionalLetter;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class ProvisionalLetterManager: IProvisionalLetterManager
{
    private readonly IProvisionalLetterRepository _provisionalLetterRepository;
    private readonly IFleetManager _fleetManager;
    public ProvisionalLetterManager(IProvisionalLetterRepository provisionalLetterRepository, IFleetManager fleetManager)
    {
        _provisionalLetterRepository = provisionalLetterRepository;
        _fleetManager = fleetManager;
    }
    public async Task<ProvisionalLetteResponse> GenerateprovisionalLetter(long FleetID)
    {
        LetterMasterDataResponse letterMasterDataResponse = await _fleetManager.LetterMasterData(FleetID);

        ProvisionalLetteResponse provisionalLetteResponse = new ProvisionalLetteResponse();

        if (letterMasterDataResponse != null && !string.IsNullOrEmpty(letterMasterDataResponse.BorrowerName))
        {
            ProvisionalLetterRequestModel provisionalLetterRequestModel = new ProvisionalLetterRequestModel();
            provisionalLetterRequestModel.Name = letterMasterDataResponse.BorrowerName ?? "";
            provisionalLetterRequestModel.ProcessingFee = letterMasterDataResponse.ProcessingFees ?? 0;
            provisionalLetterRequestModel.ApplicationNumber = FleetID;
            provisionalLetterRequestModel.LoanAmount = letterMasterDataResponse.TotalAmountofLoan ?? 0;
            provisionalLetterRequestModel.LoanTenure = 12;
            provisionalLetterRequestModel.RateOfInterest = letterMasterDataResponse.InterestRate ?? 0;

            ProvisionalLetteResponseModel provisionalLetteResponseModel = await _provisionalLetterRepository.GenerateprovisionalLetter(provisionalLetterRequestModel);

            provisionalLetteResponse.Letter = provisionalLetteResponseModel.Letter;
        }
        return provisionalLetteResponse;
    }
}
