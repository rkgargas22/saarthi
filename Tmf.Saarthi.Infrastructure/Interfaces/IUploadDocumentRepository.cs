using Tmf.Saarthi.Infrastructure.Models.Request.Document;
using Tmf.Saarthi.Infrastructure.Models.Response.Document;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IUploadDocumentRepository
{
    Task<DocumentResponseModel> AddDocument(DocumentRequestModel documentRequestModel);
}
