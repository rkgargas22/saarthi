using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.ResponseModels.Document;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IUploadManager
{
    Task<DocumentResponse> AddDocument(DocumentRequest documentRequest);
    Task<List<DownloadDocumentResponse>> DownloadDocument(long fleetId, int stageId, int DocTypeId);
    Task<UploadDocumentsResponse> UploadDocuments(UploadDocumentsRequest uploadDocumentsRequest,long createdBy);
}
