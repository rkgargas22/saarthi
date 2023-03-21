using Tmf.Saarthi.Core.RequestModels.DMS;
using Tmf.Saarthi.Core.RequestModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Infrastructure.Models.Request.DMS;
using Tmf.Saarthi.Infrastructure.Models.Response.DMS;

namespace Tmf.Saarthi.Infrastructure.Interfaces;

public interface IDMSRepository
{
    Task<GenerateFanNoResponseModel> GenerateFanNo(GenerateFanNoRequestModel generateFanNoRequestModel);
    Task<GenerateTokenResponseModel> GenerateToken(GenerateTokenRequestModel generateTokenRequestModel);
    Task<UploadDocumentsDMSResponseModel> UploadDocument(UploadDocumentsDMSRequestModel uploadDocumentsDMSRequestModel);
    Task<DMSDocumentAndFleetResponseModel> GetFleetAndDocDetailsByFleetId(DMSDocumentAndFleetRequestModel dMSDocumentAndFleetRequestModel);
    Task<UpdateFleetFanNoResponse> UpdateFleetFanNo(UpdateFleetFanNoRequest updateFleetFanNoRequest);
    Task<UpdateDmsDocumentStatusResponseModel> UpdateDmsDocumentStatus(UpdateDmsDocumentStatusRequestModel updateDmsDocumentStatusRequestModel);
    Task<UpdateRcuResponseModel> UpdateDocumentQueue(UpdateRcuRequestModel updateRcuRequestModel);

    Task<ViewDocumentResponseModel> ViewDocument(ViewDocumentRequestModel viewDocumentRequestModel);

    Task<string> GetFanNo(GetFanNoRequestModel getFanNoRequestModel);
    

}

