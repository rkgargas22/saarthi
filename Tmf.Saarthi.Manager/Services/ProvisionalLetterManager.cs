using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Text;
using Tmf.Saarthi.Core.Enums;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.RequestModels.Letter;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.ProvisionalLette;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.ProvisionalLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.DocumentTypeMstr;
using Tmf.Saarthi.Infrastructure.Models.Response.ProvisionalLetter;
using Tmf.Saarthi.Infrastructure.Models.Response.StageMaster;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class ProvisionalLetterManager : IProvisionalLetterManager
{
    private const string TemplatesFolderName = "Templates";
    private const string LetterHtmlFileName = "ProvisionalLetter.html";

    private readonly IProvisionalLetterRepository _provisionalLetterRepository;
    private readonly IFleetManager _fleetManager;
    private readonly IUploadManager _uploadManager;
    private readonly LetterOptions _letterOptions;
    private readonly IStageMasterManager _stageMasterManager;
    private readonly IDocumentTypeMstrManager _documentTypeMstrManager;
    private readonly IHostingEnvironment _environment;

    public ProvisionalLetterManager(IProvisionalLetterRepository provisionalLetterRepository,
                                    IFleetManager fleetManager,
                                    IUploadManager uploadManager,
                                    IOptions<LetterOptions> letterOptions,
                                    IStageMasterManager stageMasterManager,
                                    IDocumentTypeMstrManager documentTypeMstrManager,
                                    IHostingEnvironment environment)
    {
        _provisionalLetterRepository = provisionalLetterRepository;
        _fleetManager = fleetManager;
        _uploadManager = uploadManager;
        _letterOptions = letterOptions.Value;
        _stageMasterManager = stageMasterManager;
        _documentTypeMstrManager = documentTypeMstrManager;
        _environment = environment;
    }
    public async Task<ProvisionalLetterResponse> GenerateProvisionalLetter(long FleetId, long CreatedBy)
    {
        LetterMasterDataResponse letterMasterDataResponse = await _fleetManager.LetterMasterData(FleetId);

        ProvisionalLetterResponse provisionalLetterResponse = new();

        if (letterMasterDataResponse != null && !string.IsNullOrEmpty(letterMasterDataResponse.BorrowerName))
        {
            string templatePath = Path.Combine(_environment.WebRootPath, TemplatesFolderName, LetterHtmlFileName);
            byte[] templateBytes = File.ReadAllBytes(templatePath);
            string base64String = Convert.ToBase64String(templateBytes);

            Dictionary<string, string> mappingProperties = GetMappingProperties(letterMasterDataResponse, FleetId);

            ProvisionalLetterRequestModel requestModel = new()
            {
                MappingProperties = mappingProperties,
                Htmlbase64String = base64String
            };

            ProvisionalLetteResponseModel provisionalLetteResponseModel = await _provisionalLetterRepository.GenerateprovisionalLetter(requestModel);

            provisionalLetterResponse.Letter = provisionalLetteResponseModel.Letter;

            if (!string.IsNullOrWhiteSpace(provisionalLetterResponse.Letter))
            {
                StageMasterResponseModel stageMasterResponseModel = await _stageMasterManager.GetStageMasterByStageCode(StageCodeFlag.PROLGE);
                DocumentTypeMstrResponseModel documentTypeMstrResponseModel = await _documentTypeMstrManager.GetDocumentTypeMstrByDocumentCode(DocumentCodeFlag.PROL);

                int StageId = stageMasterResponseModel.StageId;
                int DocTypeId = documentTypeMstrResponseModel.DocTypeId;

                DocumentRequest documentRequest = new DocumentRequest
                {
                    FleetId = FleetId,
                    DocTypeId = DocTypeId,
                    StageId = StageId,
                    Extension = ".pdf",
                    //CreatedBy = CreatedBy,
                    DocumentName = "ProvisionalLetter"
                };

                await _uploadManager.AddDocument(documentRequest);

                if (!string.IsNullOrWhiteSpace(_letterOptions.DocumentFolderPath))
                {
                    string sharedFolderPath = Path.Combine(_letterOptions.DocumentFolderPath, FleetId.ToString());
                    if (!Directory.Exists(sharedFolderPath))
                    {
                        Directory.CreateDirectory(sharedFolderPath);
                    }

                    string pdfPath = Path.Combine(sharedFolderPath, "ProvisionalLetter.pdf");

                    byte[] bytes = Convert.FromBase64String(provisionalLetterResponse.Letter);

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
                throw new ArgumentNullException("Provisional letter is not generated.");
            }
        }
        return provisionalLetterResponse;
    }

    public async Task<DisagreeProvisionalResponse> DisagreeProvisionalLetter(DisagreeProvisionalResquest disagreeProvisionalResquest)
    {
        DisagreeProvisionalResquestModel disagreeProvisionalResquestModel = new DisagreeProvisionalResquestModel();
        disagreeProvisionalResquestModel.FleetId = disagreeProvisionalResquest.FleetId;

        DisagreeProvisionalResponseModel disagreeProvisionalResponseModel = await _provisionalLetterRepository.DisagreeProvisionalLetter(disagreeProvisionalResquestModel);

        DisagreeProvisionalResponse disagreeProvisionalResponse = new DisagreeProvisionalResponse();
        if (disagreeProvisionalResponseModel.FleetId == 0)
        {
            disagreeProvisionalResponse.message = "Update Failed";
        }
        else
        {
            disagreeProvisionalResponse.message = "Updated Successfully";
        }
        return disagreeProvisionalResponse;
    }

    private Dictionary<string, string> GetMappingProperties(LetterMasterDataResponse response, long FleetId)
    {
        Dictionary<string, string> mappingProperties = new()
        {
            { "##Name", response.BorrowerName ?? string.Empty },
            { "##ApplicationNumber", Convert.ToString(FleetId) },
            { "##LoanAmount", Convert.ToString(response.TotalAmountofLoan ?? 0) },
            { "##LoanTenure", Convert.ToString(12) },
            { "##RateOfInterest", Convert.ToString(response.InterestRate ?? 0)},
            { "##ProcessingFee", Convert.ToString(response.ProcessingFees ?? 0) }
        };

        return mappingProperties;
    }
}
