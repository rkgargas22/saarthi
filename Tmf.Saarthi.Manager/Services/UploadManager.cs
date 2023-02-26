using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.ResponseModels.Document;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Document;
using Tmf.Saarthi.Infrastructure.Models.Response.Document;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class UploadManager : IUploadManager
{
    private readonly IUploadDocumentRepository _uploadDocumentRepository;
    public UploadManager(IUploadDocumentRepository uploadDocumentRepository)
    {
        _uploadDocumentRepository=  uploadDocumentRepository;
    }

    public async Task<DocumentResponse> AddDocument(DocumentRequest documentRequest)
    {
        DocumentRequestModel documentRequestModel = new DocumentRequestModel();
        documentRequestModel.FleetId = documentRequest.FleetId;
        documentRequestModel.DocumentUrl = documentRequest.DocumentUrl;
        documentRequestModel.CreatedBy = documentRequest.CreatedBy;
        documentRequestModel.IsActive = documentRequest.IsActive;
        documentRequestModel.Documenttype = documentRequest.Documenttype;
       
        DocumentResponseModel addFleetVehicleResponseModel = await _uploadDocumentRepository.AddDocument(documentRequestModel);

        DocumentResponse documentResponse = new DocumentResponse();
        documentResponse.FleetId = documentRequestModel.FleetId;

        return documentResponse;
    }
}
