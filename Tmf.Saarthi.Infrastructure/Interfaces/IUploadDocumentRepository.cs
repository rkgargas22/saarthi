using Tmf.Saarthi.Infrastructure.Models.Request.Document;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;
using Tmf.Saarthi.Infrastructure.Models.Response.Document;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IUploadDocumentRepository
{
    Task<DocumentResponseModel> AddDocument(DocumentRequestModel documentRequestModel);
    Task<List<DownloadDocumentResponseModel>> DownloadDocument(long fleetId, int stageId, int DocTypeId);
    Task<UploadDocumentsResponseModel> UploadDocuments(UploadDocumentsRequestModel uploadDocumentsRequestModel);
}
