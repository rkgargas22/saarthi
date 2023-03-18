using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Tmf.Saarthi.Core.Enums;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.SanctionLetter;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.SanctionLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.DocumentTypeMstr;
using Tmf.Saarthi.Infrastructure.Models.Response.SanctionLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.StageMaster;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class SantionLetterManager : ISanctionLetterManager
{
    private const string TemplatesFolderName = "Templates";
    private const string LetterHtmlFileName = "SanctionLetter.html";

    private readonly ISantionLetterRepository _santionLetterRepository;
    private readonly IFleetManager _fleetManager;
    private readonly IUploadManager _uploadManager;
    private readonly LetterOptions _letterOptions;
    private readonly IStageMasterManager _stageMasterManager;
    private readonly IDocumentTypeMstrManager _documentTypeMstrManager;
    private readonly IHostingEnvironment _environment;

    public SantionLetterManager(ISantionLetterRepository santionLetterRepository,
                                IFleetManager fleetManager,
                                IUploadManager uploadManager,
                                IOptions<LetterOptions> letterOptions,
                                IStageMasterManager stageMasterManager,
                                IDocumentTypeMstrManager documentTypeMstrManager,
                                IHostingEnvironment environment)
    {
        _santionLetterRepository = santionLetterRepository;
        _fleetManager = fleetManager;
        _uploadManager = uploadManager;
        _letterOptions = letterOptions.Value;
        _stageMasterManager = stageMasterManager;
        _documentTypeMstrManager = documentTypeMstrManager;
        _environment = environment;
    }

    public async Task<SanctionLetterResponse> GenerateSanctionLetter(long FleetId, long CreatedBy)
    {
        LetterMasterDataResponse letterMasterDataResponse = await _fleetManager.LetterMasterData(FleetId);

        SanctionLetterResponse sanctionLetterResponse = new();

        if (letterMasterDataResponse != null && !string.IsNullOrEmpty(letterMasterDataResponse.BorrowerName))
        {
            string templatePath = Path.Combine(_environment.WebRootPath, TemplatesFolderName, LetterHtmlFileName);
            byte[] templateBytes = File.ReadAllBytes(templatePath);
            string base64String = Convert.ToBase64String(templateBytes);

            Dictionary<string, string> mappingProperties = GetMappingProperties(letterMasterDataResponse);

            SanctionLetterRequestModel requestModel = new()
            {
                MappingProperties = mappingProperties,
                Htmlbase64String = base64String
            };

            SanctionLetterResponseModel sanctionLetterResponseModel = await _santionLetterRepository.GenerateSanctionLetter(requestModel);
            sanctionLetterResponse.Letter = sanctionLetterResponseModel.Letter;

            if (!string.IsNullOrWhiteSpace(sanctionLetterResponse.Letter))
            {
                StageMasterResponseModel stageMasterResponseModel = await _stageMasterManager.GetStageMasterByStageCode(StageCodeFlag.SANLGE);
                DocumentTypeMstrResponseModel documentTypeMstrResponseModel = await _documentTypeMstrManager.GetDocumentTypeMstrByDocumentCode(DocumentCodeFlag.SANL);

                int StageId = stageMasterResponseModel.StageId;
                int DocTypeId = documentTypeMstrResponseModel.DocTypeId;

                DocumentRequest documentRequest = new DocumentRequest
                {
                    FleetId = FleetId,
                    DocTypeId = DocTypeId,
                    StageId = StageId,
                    Extension = ".pdf",
                    //CreatedBy = CreatedBy,
                    DocumentName = "SanctionLetter"
                };

                await _uploadManager.AddDocument(documentRequest);

                if (!string.IsNullOrWhiteSpace(_letterOptions.DocumentFolderPath))
                {
                    string sharedFolderPath = Path.Combine(_letterOptions.DocumentFolderPath, FleetId.ToString());
                    if (!Directory.Exists(sharedFolderPath))
                    {
                        Directory.CreateDirectory(sharedFolderPath);
                    }

                    string pdfPath = Path.Combine(sharedFolderPath, "SanctionLetter.pdf");

                    byte[] bytes = Convert.FromBase64String(sanctionLetterResponse.Letter);

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
                throw new ArgumentNullException("Sanction letter is not generated.");
            }
        }

        return sanctionLetterResponse;
    }

    private Dictionary<string, string> GetMappingProperties(LetterMasterDataResponse response)
    {
        Dictionary<string, string> mappingProperties = new()
        {
            { "##Date", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") },
            { "##FanNo", response.FanNo },
            { "##BorrowerName", response.BorrowerName },
            { "##BorrowerAddressLine1", response.BorrowerAddressLine1 },
            { "##BorrowerAddressLine2", response.BorrowerAddressLine2 },
            { "##BorrowerAddressLine3", response.BorrowerAddressLine2 },
            { "##CoBorrowerName", response.CoBorrowerName},
            { "##CoBorrowerAddressLine1", response.CoBorrowerAddressLine1},
            { "##CoBorrowerAddressLine2", response.CoBorrowerAddressLine2},
            { "##CoBorrowerAddressLine3", response.CoBorrowerAddressLine3},
            { "##SanctionLimit", Convert.ToString(response.Limit)!},
            { "##CutOffLimit", Convert.ToString(response.CutOffLimit)!},
            { "##ProcessingFee", Convert.ToString(response.ProcessingFees)!},
            { "##StampDuty", Convert.ToString(response.StampDuty)!},
            { "##CLI", response.Cli},
            { "##Aetna", response.Aetna},
            { "##LegalExpenses", response.LegalExpenses},
            { "##ChequeBouncingCharges", Convert.ToString(response.ChequeBouncingCharges)!},
            { "##RetainerCharges", response.RetainerCharges},
            { "##InterestRate", Convert.ToString(response.InterestRate)!},
            { "##AcceleratedInterestrate", Convert.ToString(response.AcceleratedInterest)!},
            { "##BorrowerAuthorisedPersonName", response.BorrowerAuthorisedPersonName},
            { "##CoBorrowerAuthorisedPersonName", response.CoBorrowerAuthorisedPersonName}
        };

        return mappingProperties;
    }
}
