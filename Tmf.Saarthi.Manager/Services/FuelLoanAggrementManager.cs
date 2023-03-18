using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using Tmf.Saarthi.Core.Enums;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.FuelLoanAgreement;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.FuelLoanAggrement;
using Tmf.Saarthi.Infrastructure.Models.Request.SanctionLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.DocumentTypeMstr;
using Tmf.Saarthi.Infrastructure.Models.Response.FuelLoanAggrement;
using Tmf.Saarthi.Infrastructure.Models.Response.StageMaster;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class FuelLoanAggrementManager : IFuelLoanAggrementManager
{
    private const string TemplatesFolderName = "Templates";
    private const string LetterHtmlFileName = "FuelLoanAgreement.html";

    private readonly IFuelLoanAggrementRepository _fuelLoanAggrementRepository;
    private readonly IFleetManager _fleetManager;
    private readonly IUploadManager _uploadManager;
    private readonly LetterOptions _letterOptions;
    private readonly IStageMasterManager _stageMasterManager;
    private readonly IDocumentTypeMstrManager _documentTypeMstrManager;
    private readonly IHostingEnvironment _environment;

    public FuelLoanAggrementManager(IFuelLoanAggrementRepository fuelLoanAggrementRepository,
                                    IFleetManager fleetManager,
                                    IUploadManager uploadManager,
                                    IOptions<LetterOptions> letterOptions,
                                    IStageMasterManager stageMasterManager,
                                    IDocumentTypeMstrManager documentTypeMstrManager,
                                    IHostingEnvironment environment)
    {
        _fuelLoanAggrementRepository = fuelLoanAggrementRepository;
        _fleetManager = fleetManager;
        _uploadManager = uploadManager;
        _letterOptions = letterOptions.Value;
        _stageMasterManager = stageMasterManager;
        _documentTypeMstrManager = documentTypeMstrManager;
        _environment = environment;
    }
    public async Task<FuelLoanAgreementResponse> GenerateFuelLoanAgreement(long FleetId, long CreatedBy)
    {
        LetterMasterDataResponse letterMasterDataResponse = await _fleetManager.LetterMasterData(FleetId);

        FuelLoanAgreementResponse fuelLoanAgreementResponse = new();

        if (letterMasterDataResponse != null && !string.IsNullOrEmpty(letterMasterDataResponse.BorrowerName))
        {
            string templatePath = Path.Combine(_environment.WebRootPath, TemplatesFolderName, LetterHtmlFileName);
            byte[] templateBytes = File.ReadAllBytes(templatePath);
            string base64String = Convert.ToBase64String(templateBytes);

            Dictionary<string, string> mappingProperties = GetMappingProperties(letterMasterDataResponse);
            FuelLoanAggrementRequestModel requestModel = new()
            {
                MappingProperties = mappingProperties,
                Htmlbase64String = base64String
            };

            FuelLoanAggrementResponseModel fuelLoanAggrementResponseModel = await _fuelLoanAggrementRepository.GenerateFuelLoanAgreement(requestModel);

            fuelLoanAgreementResponse.Letter = fuelLoanAggrementResponseModel.Letter;

            if (!string.IsNullOrWhiteSpace(fuelLoanAgreementResponse.Letter))
            {
                StageMasterResponseModel stageMasterResponseModel = await _stageMasterManager.GetStageMasterByStageCode(StageCodeFlag.AGLGEN);
                DocumentTypeMstrResponseModel documentTypeMstrResponseModel = await _documentTypeMstrManager.GetDocumentTypeMstrByDocumentCode(DocumentCodeFlag.AGRL);

                int StageId = stageMasterResponseModel.StageId;
                int DocTypeId = documentTypeMstrResponseModel.DocTypeId;

                DocumentRequest documentRequest = new DocumentRequest
                {
                    FleetId = FleetId,
                    DocTypeId = DocTypeId,
                    StageId = StageId,
                    Extension = ".pdf",
                    //CreatedBy = CreatedBy,
                    DocumentName = "FuelLoanAgreement"
                };

                await _uploadManager.AddDocument(documentRequest);

                if (!string.IsNullOrWhiteSpace(_letterOptions.DocumentFolderPath))
                {
                    string sharedFolderPath = Path.Combine(_letterOptions.DocumentFolderPath, FleetId.ToString());
                    if (!Directory.Exists(sharedFolderPath))
                    {
                        Directory.CreateDirectory(sharedFolderPath);
                    }

                    string pdfPath = Path.Combine(sharedFolderPath, "FuelLoanAgreementLetter.pdf");

                    byte[] bytes = Convert.FromBase64String(fuelLoanAgreementResponse.Letter);

                    using FileStream stream = new FileStream(pdfPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    using BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();

                }
                else
                {
                    throw new ArgumentNullException("Document path is not configured");
                }
            }
            else
            {
                throw new ArgumentNullException("FuelLoanAgreement letter is not generated.");
            }
        }

        return fuelLoanAgreementResponse;
    }

    private Dictionary<string, string> GetMappingProperties(LetterMasterDataResponse response)
    {
        Dictionary<string, string> mappingProperties = new()
        {
            {"##BorrowerAuthorisedPersonName", response.BorrowerAuthorisedPersonName},
            {"##CoBorrowerAuthorisedPersonName", response.CoBorrowerAuthorisedPersonName},
            {"##DateofAgreement", Convert.ToString(response.AgreementDate)!},
            {"##PlaceoftheAgreement", response.AgreementPlace},
            {"##FileAccountNumber", Convert.ToString(response.FileAccountNumber)},
            {"##Addressoftheconcernedoffice/branch", response.OfficeOrbranchAddress},
            {"##BorrowerName", response.BorrowerName},
            {"##BorrowerConstitution", response.BorrowerConstitution},
            {"##BorrowerAddress", string.Concat(response.BorrowerAddressLine1, " ", response.BorrowerAddressLine2 + " " + response.BorrowerAddressLine3)},
            {"##BorrowerMobileNumber", response.BorrowerMobileNumber},
            {"##BorrowerEmailID", response.BorrowerEmailID},
            {"##CoBorrowerName", response.CoBorrowerName},
            {"##CoBorrowerConstitution", response.CoBorrowerConstitution},
            {"##CoBorrowerAddress", string.Concat(response.CoBorrowerAddressLine1 + " " + response.CoBorrowerAddressLine2 + " " + response.CoBorrowerAddressLine3)},
            {"##CoBorrowerMobileNumber", response.CoBorrowerMobileNumber},
            {"##CoBorrowerEmailID", response.CoBorrowerEmailID},
            {"##TotalAmountofLoan", Convert.ToString(response.TotalAmountofLoan)},
            {"##Limit", Convert.ToString(response.Limit)},
            {"##CutOffLimit", Convert.ToString(response.CutOffLimit)},
            {"##InterestRate", Convert.ToString(response.InterestRate)},
            {"##TypeofInterest", response.TypeofInterest},
            {"##AcceleratedInterest", Convert.ToString(response.AcceleratedInterest)},
            {"##PurposeoftheLoan", response.PurposeoftheLoan},
            {"##AvailabilityPeriod", response.AvailabilityPeriod},
            {"##NameoftheOilCompany", response.OilCompanyName},
            {"##NameoftheFuelProgramme", response.FuelProgrammeName},
            {"##DesignatedAccountoftheOilCompany", response.OilCompanyDesignatedAccount},
            {"##LegalExpenses", response.LegalExpenses},
            {"##ServiceCharges", response.ServiceCharges},
            {"##ProcessingFees", Convert.ToString(response.ProcessingFees)},
            {"##StampDuty", Convert.ToString(response.StampDuty)},
            {"##CLI", response.Cli},
            {"##AETNA", response.Aetna},
            {"##OtherCharges", response.OtherCharges},
        };

        return mappingProperties;
    }
}
