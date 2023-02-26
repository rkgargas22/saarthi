using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.SanctionLetter;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.SanctionLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.SanctionLetter;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class SantionLetterManager : ISanctionLetterManager
{
    private readonly ISantionLetterRepository _santionLetterRepository;
    private readonly IFleetManager _fleetManager;
    public SantionLetterManager(ISantionLetterRepository santionLetterRepository, IFleetManager fleetManager)
    {
        _santionLetterRepository = santionLetterRepository;
        _fleetManager = fleetManager;
    }
    public async Task<SanctionLetterResponse> GenerateSanctionLetter(long FleetID)
    {
        LetterMasterDataResponse letterMasterDataResponse = await _fleetManager.LetterMasterData(FleetID);

        SanctionLetterResponse sanctionLetterResponse = new SanctionLetterResponse();

        if (letterMasterDataResponse != null && !string.IsNullOrEmpty(letterMasterDataResponse.BorrowerName))
        {
            SanctionLetterRequestModel sanctionLetterRequestModel = new SanctionLetterRequestModel();
            sanctionLetterRequestModel.FanNo = letterMasterDataResponse.FanNo;
            sanctionLetterRequestModel.BorrowerName = letterMasterDataResponse.BorrowerName;
            sanctionLetterRequestModel.BorrowerAddressLine1 = letterMasterDataResponse.BorrowerAddressLine1;
            sanctionLetterRequestModel.BorrowerAddressLine2 = letterMasterDataResponse.BorrowerAddressLine2;
            sanctionLetterRequestModel.BorrowerAddressLine3 = letterMasterDataResponse.BorrowerAddressLine3;
            sanctionLetterRequestModel.CoBorrowerName = letterMasterDataResponse.CoBorrowerName;
            sanctionLetterRequestModel.CoBorrowerAddressLine1 = letterMasterDataResponse.CoBorrowerAddressLine1;
            sanctionLetterRequestModel.CoBorrowerAddressLine2 = letterMasterDataResponse.CoBorrowerAddressLine2;
            sanctionLetterRequestModel.CoBorrowerAddressLine3 = letterMasterDataResponse.CoBorrowerAddressLine3;
            sanctionLetterRequestModel.SanctionLimit = letterMasterDataResponse.Limit;
            sanctionLetterRequestModel.CutOffLimit = letterMasterDataResponse.CutOffLimit;
            sanctionLetterRequestModel.ProcessingFee = letterMasterDataResponse.ProcessingFees;
            sanctionLetterRequestModel.StampDuty = letterMasterDataResponse.StampDuty;
            sanctionLetterRequestModel.CLI = letterMasterDataResponse.Cli;
            sanctionLetterRequestModel.Aetna = letterMasterDataResponse.Aetna;
            sanctionLetterRequestModel.LegalExpenses = letterMasterDataResponse.LegalExpenses;
            sanctionLetterRequestModel.ChequeBouncingCharges = letterMasterDataResponse.ChequeBouncingCharges;
            sanctionLetterRequestModel.RetainerCharges = letterMasterDataResponse.RetainerCharges;
            sanctionLetterRequestModel.InterestRate = letterMasterDataResponse.InterestRate;
            sanctionLetterRequestModel.AcceleratedInterestrate = letterMasterDataResponse.AcceleratedInterest;
            sanctionLetterRequestModel.BorrowerAuthorisedPersonName = letterMasterDataResponse.BorrowerAuthorisedPersonName;
            sanctionLetterRequestModel.CoBorrowerAuthorisedPersonName = letterMasterDataResponse.CoBorrowerAuthorisedPersonName;

            SanctionLetterResponseModel sanctionLetterResponseModel = await _santionLetterRepository.GenerateSanctionLetter(sanctionLetterRequestModel);

            sanctionLetterResponse.Letter = sanctionLetterResponseModel.Letter;
        }

        return sanctionLetterResponse;
    }
}
