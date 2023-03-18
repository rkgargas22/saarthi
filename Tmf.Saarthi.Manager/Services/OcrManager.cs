using Tmf.Saarthi.Core.Constants;
using Tmf.Saarthi.Core.RequestModels.Ocr;
using Tmf.Saarthi.Core.ResponseModels.Ocr;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Ocr;
using Tmf.Saarthi.Infrastructure.Models.Response.Ocr;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class OcrManager : IOcrManager
{
    private readonly IOcrRepository _ocrRepository;
    public OcrManager(IOcrRepository ocrRepository)
    {
        _ocrRepository = ocrRepository;
    }

    public async Task<AddressDetailsResponse> GetAddressDetails(AddressDetailsRequest getAddressDetailsRequest)
    {
       // bool isValidDoc = await ValidateDocument(getAddressDetailsRequest);
        AddressDetailsResponse getAddressDetailsResponse = new AddressDetailsResponse();

        if (true)
        {
            if (getAddressDetailsRequest.DocumentType == (int)OCRDocuments.Aadhar)
            {
                AadharExtractResponseModel aadharExtractResponseModel = await AadharExtract(getAddressDetailsRequest);
                if (aadharExtractResponseModel != null && aadharExtractResponseModel.Status == "completed")
                {
                    if (aadharExtractResponseModel.Result != null && aadharExtractResponseModel.Result.ExtractionOutput != null)
                    {
                        getAddressDetailsResponse.BPNo = getAddressDetailsRequest.BpNo;
                        getAddressDetailsResponse.Address = aadharExtractResponseModel.Result.ExtractionOutput.Address;
                        getAddressDetailsResponse.StreetAddress = aadharExtractResponseModel.Result.ExtractionOutput.StreetAddress;
                        getAddressDetailsResponse.District = aadharExtractResponseModel.Result.ExtractionOutput.District;
                        getAddressDetailsResponse.Pincode = aadharExtractResponseModel.Result.ExtractionOutput.Pincode;
                        getAddressDetailsResponse.State = aadharExtractResponseModel.Result.ExtractionOutput.State;
                    }
                    else if (aadharExtractResponseModel.Result != null && aadharExtractResponseModel.Result.QrOutput != null)
                    {
                        getAddressDetailsResponse.BPNo = getAddressDetailsRequest.BpNo;
                        getAddressDetailsResponse.Address = aadharExtractResponseModel.Result.QrOutput.Address;
                        getAddressDetailsResponse.StreetAddress = aadharExtractResponseModel.Result.QrOutput.StreetAddress;
                        getAddressDetailsResponse.District = aadharExtractResponseModel.Result.QrOutput.District;
                        getAddressDetailsResponse.Pincode = aadharExtractResponseModel.Result.QrOutput.Pincode;
                        getAddressDetailsResponse.State = aadharExtractResponseModel.Result.QrOutput.State;
                    }
                }
            }
            else if (getAddressDetailsRequest.DocumentType == (int)OCRDocuments.DrivingLicence)
            {
                DLExtractResponseModel dLExtractResponseModel = await DLExtract(getAddressDetailsRequest);
                if (dLExtractResponseModel != null && dLExtractResponseModel.Status == "completed")
                {
                    if (dLExtractResponseModel.Result != null && dLExtractResponseModel.Result.ExtractionOutput != null)
                    {
                        getAddressDetailsResponse.BPNo = getAddressDetailsRequest.BpNo;
                        getAddressDetailsResponse.Address = dLExtractResponseModel.Result.ExtractionOutput.Address;
                        getAddressDetailsResponse.StreetAddress = dLExtractResponseModel.Result.ExtractionOutput.StreetAddress;
                        getAddressDetailsResponse.District = dLExtractResponseModel.Result.ExtractionOutput.District;
                        getAddressDetailsResponse.Pincode = dLExtractResponseModel.Result.ExtractionOutput.Pincode;
                        getAddressDetailsResponse.State = dLExtractResponseModel.Result.ExtractionOutput.State;
                    }
                }
            }
            else if (getAddressDetailsRequest.DocumentType == (int)OCRDocuments.VoterId)
            {
                VoterIdExtractResponseModel voterIdExtractResponseModel = await VoterIdExtract(getAddressDetailsRequest);
                if (voterIdExtractResponseModel != null && voterIdExtractResponseModel.Status == "completed")
                {
                    if (voterIdExtractResponseModel.Result != null && voterIdExtractResponseModel.Result.ExtractionOutput != null)
                    {
                        getAddressDetailsResponse.BPNo = getAddressDetailsRequest.BpNo;
                        getAddressDetailsResponse.Address = voterIdExtractResponseModel.Result.ExtractionOutput.Address;
                        getAddressDetailsResponse.StreetAddress = voterIdExtractResponseModel.Result.ExtractionOutput.StreetAddress;
                        getAddressDetailsResponse.District = voterIdExtractResponseModel.Result.ExtractionOutput.District;
                        getAddressDetailsResponse.Pincode = voterIdExtractResponseModel.Result.ExtractionOutput.Pincode;
                        getAddressDetailsResponse.State = voterIdExtractResponseModel.Result.ExtractionOutput.State;
                    }
                }
            }
            OCRUploadDocumentResponseModel oCRUploadDocumentResponseModel = await UploadOcrDocuments(getAddressDetailsRequest);
            OCRUploadAddressResponseModel oCRUploadAddressResponseModel = await UploadOcrAddress(getAddressDetailsResponse);

        }
        return getAddressDetailsResponse;
    }

    private async Task<bool> ValidateDocument(AddressDetailsRequest getAddressDetailsRequest)
    {
        bool validDoc1 = false;
        bool validDoc2 = true;
        ValidateDocumentRequestModel validateDocumentRequestModel = new ValidateDocumentRequestModel();
        validateDocumentRequestModel.GroupId = getAddressDetailsRequest.BpNo.ToString();
        validateDocumentRequestModel.TaskId = getAddressDetailsRequest.FleetID.ToString();

        validateDocumentRequestModel.Data = new Data();
        validateDocumentRequestModel.Data.Document1 = getAddressDetailsRequest.FrontPage;
        validateDocumentRequestModel.Data.DocType = getAddressDetailsRequest.DocumentType;
        validateDocumentRequestModel.Data.AdvancedFeatures = new AdvancedFeatures();
        validateDocumentRequestModel.Data.AdvancedFeatures.DetectDocSide = true;

        ValidateDocumentResponseModel validateDocumentResponseModel = await _ocrRepository.ValidateDocument(validateDocumentRequestModel);

        if (validateDocumentResponseModel.Status == "completed" && validateDocumentResponseModel.Result != null && validateDocumentResponseModel.Result.IsReadable)
        {
            validDoc1 = true;
        }

        if (!string.IsNullOrEmpty(getAddressDetailsRequest.BackPage))
        {
            validDoc2 = false;
            ValidateDocumentRequestModel validateDocumentRequestModel2 = validateDocumentRequestModel;
            validateDocumentRequestModel2.Data.Document1 = getAddressDetailsRequest.BackPage;

            ValidateDocumentResponseModel validateDocumentResponseModel2 = await _ocrRepository.ValidateDocument(validateDocumentRequestModel2);

            if (validateDocumentResponseModel2.Status == "completed" && validateDocumentResponseModel2.Result != null && validateDocumentResponseModel2.Result.IsReadable)
            {
                validDoc2 = true;
            }
        }

        if (validDoc1 && validDoc2)
        {
            return true;
        }
        return false;

    }

    private async Task<AadharExtractResponseModel> AadharExtract(AddressDetailsRequest getAddressDetailsRequest)
    {
        AadharExtractRequestModel aadharExtractRequestModel = new AadharExtractRequestModel();
        aadharExtractRequestModel.GroupId = getAddressDetailsRequest.BpNo.ToString();
        aadharExtractRequestModel.TaskId = getAddressDetailsRequest.FleetID.ToString();
        aadharExtractRequestModel.Data = new DataAadhar();
        aadharExtractRequestModel.Data.Document1 = getAddressDetailsRequest.FrontPage;
        aadharExtractRequestModel.Data.Document2 = getAddressDetailsRequest.BackPage;
        aadharExtractRequestModel.Data.Consent = "yes";

        AadharExtractResponseModel aadharExtractResponseModel = await _ocrRepository.AadharExtract(aadharExtractRequestModel);

        return aadharExtractResponseModel;
    }

    private async Task<DLExtractResponseModel> DLExtract(AddressDetailsRequest getAddressDetailsRequest)
    {
        DLExtractRequestModel dLExtractRequestModel = new DLExtractRequestModel();
        dLExtractRequestModel.GroupId = getAddressDetailsRequest.BpNo.ToString();
        dLExtractRequestModel.TaskId = getAddressDetailsRequest.FleetID.ToString();
        dLExtractRequestModel.Data = new DataDL();
        dLExtractRequestModel.Data.Document1 = getAddressDetailsRequest.FrontPage;
        dLExtractRequestModel.Data.Document2 = getAddressDetailsRequest.BackPage;

        DLExtractResponseModel dLExtractResponseModel = await _ocrRepository.DLExtract(dLExtractRequestModel);

        return dLExtractResponseModel;
    }

    private async Task<VoterIdExtractResponseModel> VoterIdExtract(AddressDetailsRequest getAddressDetailsRequest)
    {
        VoterIdExtractRequestModel voterIdExtractRequestModel = new VoterIdExtractRequestModel();
        voterIdExtractRequestModel.GroupId = getAddressDetailsRequest.BpNo.ToString();
        voterIdExtractRequestModel.TaskId = getAddressDetailsRequest.FleetID.ToString();
        voterIdExtractRequestModel.Data = new DataVoterId();
        voterIdExtractRequestModel.Data.Document1 = getAddressDetailsRequest.FrontPage;

        VoterIdExtractResponseModel voterIdExtractResponseModel = await _ocrRepository.VoterIdExtract(voterIdExtractRequestModel);

        return voterIdExtractResponseModel;
    }

    private async Task<OCRUploadDocumentResponseModel> UploadOcrDocuments(AddressDetailsRequest getAddressDetailsRequest)
    {
        OCRUploadDocumentRequestModel oCRUploadDocumentRequestModel = new OCRUploadDocumentRequestModel();
        oCRUploadDocumentRequestModel.FleetID = getAddressDetailsRequest.FleetID;
        oCRUploadDocumentRequestModel.StagId = getAddressDetailsRequest.StagId;
        oCRUploadDocumentRequestModel.DocumentType = getAddressDetailsRequest.DocumentType;
        oCRUploadDocumentRequestModel.DocumentExtension = getAddressDetailsRequest.DocumentExtension;
        oCRUploadDocumentRequestModel.DocumentURL_1 = getAddressDetailsRequest.FrontPage;
        oCRUploadDocumentRequestModel.DocumentURL_2 = getAddressDetailsRequest.BackPage;

        OCRUploadDocumentResponseModel voterIdExtractResponseModel = await _ocrRepository.UploadOcrDocuments(oCRUploadDocumentRequestModel);

        return voterIdExtractResponseModel;
    }

    private async Task<OCRUploadAddressResponseModel> UploadOcrAddress(AddressDetailsResponse addressDetailsResponse)
    {
        OCRUploadAddressRequestModel oCRUploadAddressRequestModel = new OCRUploadAddressRequestModel();
        oCRUploadAddressRequestModel.BPNo = addressDetailsResponse.BPNo;
        oCRUploadAddressRequestModel.Address = addressDetailsResponse.Address;
        oCRUploadAddressRequestModel.StreetAddress = addressDetailsResponse.StreetAddress;
        oCRUploadAddressRequestModel.District = addressDetailsResponse.District;
        oCRUploadAddressRequestModel.Pincode = addressDetailsResponse.Pincode;
        oCRUploadAddressRequestModel.State = addressDetailsResponse.State;

        OCRUploadAddressResponseModel oCRUploadAddressResponseModel = await _ocrRepository.UploadOcrAddress(oCRUploadAddressRequestModel);

        return oCRUploadAddressResponseModel;
    }
}
