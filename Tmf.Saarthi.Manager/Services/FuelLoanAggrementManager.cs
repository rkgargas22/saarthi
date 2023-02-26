using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.FuelLoanAgreement;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.FuelLoanAggrement;
using Tmf.Saarthi.Infrastructure.Models.Response.FuelLoanAggrement;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class FuelLoanAggrementManager : IFuelLoanAggrementManager
{
    private readonly IFuelLoanAggrementRepository _fuelLoanAggrementRepository;
    private readonly IFleetManager _fleetManager;
    public FuelLoanAggrementManager(IFuelLoanAggrementRepository fuelLoanAggrementRepository, IFleetManager fleetManager)
    {
        _fuelLoanAggrementRepository = fuelLoanAggrementRepository;
        _fleetManager = fleetManager;
    }
    public async Task<FuelLoanAgreementResponse> GenerateFuelLoanAgreement(long FleetID)
    {
        LetterMasterDataResponse letterMasterDataResponse = await _fleetManager.LetterMasterData(FleetID);

        FuelLoanAgreementResponse fuelLoanAgreementResponse = new FuelLoanAgreementResponse();

        if (letterMasterDataResponse != null && !string.IsNullOrEmpty(letterMasterDataResponse.BorrowerName))
        {

            FuelLoanAggrementRequestModel fuelLoanAggrementRequestModel = new FuelLoanAggrementRequestModel();
            fuelLoanAggrementRequestModel.BorrowerAuthorisedPersonName = letterMasterDataResponse.BorrowerAuthorisedPersonName;
            fuelLoanAggrementRequestModel.CoBorrowerAuthorisedPersonName = letterMasterDataResponse.CoBorrowerAuthorisedPersonName;
            fuelLoanAggrementRequestModel.AgreementDate = letterMasterDataResponse.AgreementDate;
            fuelLoanAggrementRequestModel.AgreementPlace = letterMasterDataResponse.AgreementPlace;
            fuelLoanAggrementRequestModel.FileAccountNumber = letterMasterDataResponse.FileAccountNumber;
            fuelLoanAggrementRequestModel.OfficeOrbranchAddress = letterMasterDataResponse.OfficeOrbranchAddress;
            fuelLoanAggrementRequestModel.BorrowerName = letterMasterDataResponse.BorrowerName;
            fuelLoanAggrementRequestModel.BorrowerConstitution = letterMasterDataResponse.BorrowerConstitution;
            fuelLoanAggrementRequestModel.BorrowerAddress = string.Concat(letterMasterDataResponse.BorrowerAddressLine1, " ", letterMasterDataResponse.BorrowerAddressLine2 + " " + letterMasterDataResponse.BorrowerAddressLine3);
            fuelLoanAggrementRequestModel.BorrowerMobileNumber = letterMasterDataResponse.BorrowerMobileNumber;
            fuelLoanAggrementRequestModel.BorrowerEmailID = letterMasterDataResponse.BorrowerEmailID;
            fuelLoanAggrementRequestModel.CoBorrowerName = letterMasterDataResponse.CoBorrowerName;
            fuelLoanAggrementRequestModel.CoBorrowerConstitution = letterMasterDataResponse.CoBorrowerConstitution;
            fuelLoanAggrementRequestModel.CoBorrowerAddress = string.Concat(letterMasterDataResponse.CoBorrowerAddressLine1 + " " + letterMasterDataResponse.CoBorrowerAddressLine2 + " " + letterMasterDataResponse.CoBorrowerAddressLine3);
            fuelLoanAggrementRequestModel.CoBorrowerMobileNumber = letterMasterDataResponse.CoBorrowerMobileNumber;
            fuelLoanAggrementRequestModel.CoBorrowerEmailID = letterMasterDataResponse.CoBorrowerEmailID;
            fuelLoanAggrementRequestModel.TotalAmountofLoan = letterMasterDataResponse.TotalAmountofLoan;
            fuelLoanAggrementRequestModel.Limit = letterMasterDataResponse.Limit;
            fuelLoanAggrementRequestModel.CutOffLimit = letterMasterDataResponse.CutOffLimit;
            fuelLoanAggrementRequestModel.InterestRate = letterMasterDataResponse.InterestRate;
            fuelLoanAggrementRequestModel.TypeofInterest = letterMasterDataResponse.TypeofInterest;
            fuelLoanAggrementRequestModel.AcceleratedInterest = letterMasterDataResponse.AcceleratedInterest;
            fuelLoanAggrementRequestModel.PurposeoftheLoan = letterMasterDataResponse.PurposeoftheLoan;
            fuelLoanAggrementRequestModel.AvailabilityPeriod = letterMasterDataResponse.AvailabilityPeriod;
            fuelLoanAggrementRequestModel.OilCompanyName = letterMasterDataResponse.OilCompanyName;
            fuelLoanAggrementRequestModel.FuelProgrammeName = letterMasterDataResponse.FuelProgrammeName;
            fuelLoanAggrementRequestModel.OilCompanyDesignatedAccount = letterMasterDataResponse.OilCompanyDesignatedAccount;
            fuelLoanAggrementRequestModel.LegalExpenses = letterMasterDataResponse.LegalExpenses;
            fuelLoanAggrementRequestModel.ServiceCharges = letterMasterDataResponse.ServiceCharges;
            fuelLoanAggrementRequestModel.ProcessingFees = letterMasterDataResponse.ProcessingFees;
            fuelLoanAggrementRequestModel.StampDuty = letterMasterDataResponse.StampDuty;
            fuelLoanAggrementRequestModel.CLI = letterMasterDataResponse.Cli;
            fuelLoanAggrementRequestModel.AETNA = letterMasterDataResponse.Aetna;
            fuelLoanAggrementRequestModel.OtherCharges = letterMasterDataResponse.OtherCharges;

            FuelLoanAggrementResponseModel fuelLoanAggrementResponseModel = await _fuelLoanAggrementRepository.GenerateFuelLoanAgreement(fuelLoanAggrementRequestModel);

            fuelLoanAgreementResponse.Letter = fuelLoanAggrementResponseModel.Letter;
        }

        return fuelLoanAgreementResponse;
    }
}
