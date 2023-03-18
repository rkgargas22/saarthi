using Tmf.Saarthi.Infrastructure.Models.Request.Ocr;
using Tmf.Saarthi.Infrastructure.Models.Response.Ocr;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IOcrRepository
{
    Task<ValidateDocumentResponseModel> ValidateDocument(ValidateDocumentRequestModel validateDocumentRequestModel);

    Task<AadharExtractResponseModel> AadharExtract(AadharExtractRequestModel aadharExtractRequestModel);

    Task<DLExtractResponseModel> DLExtract(DLExtractRequestModel dlExtractRequestModel);

    Task<VoterIdExtractResponseModel> VoterIdExtract(VoterIdExtractRequestModel voterIdExtractRequestModel);

    Task<DocumentMaskingResponseModel> DocumentMasking(DocumentMaskingRequestModel documentMaskingRequestModel);
    Task<OCRUploadDocumentResponseModel> UploadOcrDocuments(OCRUploadDocumentRequestModel oCRUploadDocumentRequestModel);
    Task<OCRUploadAddressResponseModel> UploadOcrAddress(OCRUploadAddressRequestModel oCRUploadAddressRequestModel);
}
