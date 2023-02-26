using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.ResponseModels.Document;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IUploadManager
{
    Task<DocumentResponse> AddDocument(DocumentRequest documentRequest);
}
