using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Diagnostics.Metrics;
using System.IO;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.RequestModels.CPCFacility;
using Tmf.Saarthi.Core.RequestModels.DMS;
using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.RequestModels.Fleet;
using Tmf.Saarthi.Core.ResponseModels.DMS;
using Tmf.Saarthi.Core.ResponseModels.Fleet;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request;
using Tmf.Saarthi.Infrastructure.Models.Request.DMS;
using Tmf.Saarthi.Infrastructure.Models.Response.DMS;
using Tmf.Saarthi.Infrastructure.Models.Response.Fleet;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services;

public class DMSManager : IDMSManager
{   
    private readonly IDMSRepository _dMSRepository;    
    private readonly DMSOptions _dMSOptions;
    private readonly LetterOptions _letterOptions;
    public DMSManager(IDMSRepository dMSRepository,  IOptions<DMSOptions> dMSOptions,IOptions<LetterOptions> letterOptions)
    {
        _dMSRepository = dMSRepository;
        _dMSOptions = dMSOptions.Value;
        _letterOptions= letterOptions.Value;        
    }

    public async Task<GenerateFanNoResponse> GenerateFanNo(GenerateFanNoRequest generateFanNoRequest)
    {
        GenerateFanNoRequestModel generateFanNoRequestModel = new GenerateFanNoRequestModel();
        generateFanNoRequestModel.BranchCode = generateFanNoRequest.BranchCode;
        generateFanNoRequestModel.ProcessType = generateFanNoRequest.ProcessType;
        generateFanNoRequestModel.SchemeName = generateFanNoRequest.SchemeName;
        generateFanNoRequestModel.LoanType = generateFanNoRequest.LoanType;
        generateFanNoRequestModel.ApplicantName = generateFanNoRequest.ApplicantName;
        generateFanNoRequestModel.BdmName = generateFanNoRequest.BdmName;
        generateFanNoRequestModel.DsaName = generateFanNoRequest.DsaName;
        generateFanNoRequestModel.DealerName = generateFanNoRequest.DealerName;
        generateFanNoRequestModel.DealerCode = generateFanNoRequest.DealerCode;

        GenerateFanNoResponseModel generateFanNoResponseModel = await _dMSRepository.GenerateFanNo(generateFanNoRequestModel);

        GenerateFanNoResponse generateFanNoResponse = new GenerateFanNoResponse();
        generateFanNoResponse.FanNo = generateFanNoResponseModel.Fanno;

        return generateFanNoResponse;
    }

    public async Task<UploadDocumentsDMSResponse> UploadDocuments(UploadDocumentsDMSRequest uploadDocumentsDMSRequest)
    {
        UploadDocumentsDMSResponse uploadDocumentsDMSResponse = new UploadDocumentsDMSResponse();

        DMSDocumentAndFleetRequestModel dMSDocumentAndFleetRequestModel = new DMSDocumentAndFleetRequestModel();
        dMSDocumentAndFleetRequestModel.FleetId = uploadDocumentsDMSRequest.FleetId;
        dMSDocumentAndFleetRequestModel.IsRCUUpload = uploadDocumentsDMSRequest.IsRCUUpload;
        //check FAN No. exists or not        
        DMSDocumentAndFleetResponseModel dMSDocumentAndFleetResponseModel = await _dMSRepository.GetFleetAndDocDetailsByFleetId(dMSDocumentAndFleetRequestModel);
        string FanNo = Convert.ToString(dMSDocumentAndFleetResponseModel.DmsFleetDetailResponseModel.FanNo);
        string ApplicantName = Convert.ToString(dMSDocumentAndFleetResponseModel.DmsFleetDetailResponseModel.ApplicantName);
        double BranchCode = dMSDocumentAndFleetResponseModel.DmsFleetDetailResponseModel.BranchCode;       
        if (dMSDocumentAndFleetResponseModel.DmsDocumentDetailsResponseModels?.Count > 0 && string.IsNullOrEmpty(FanNo) && !string.IsNullOrEmpty(ApplicantName) && BranchCode != 0)
        {
            // Generate FAN No.
            GenerateFanNoRequest generateFanNoRequest = new GenerateFanNoRequest();
            generateFanNoRequest.BranchCode = Convert.ToString(BranchCode);
            generateFanNoRequest.ProcessType = "TMFD";
            generateFanNoRequest.SchemeName = uploadDocumentsDMSRequest.IsRCUUpload ? "RCUFL" : "NRCUFL";
            generateFanNoRequest.LoanType = "autoCV";
            generateFanNoRequest.ApplicantName = ApplicantName;
            generateFanNoRequest.BdmName = "";
            generateFanNoRequest.DsaName = "";
            generateFanNoRequest.DealerName = "DIRECT";
            generateFanNoRequest.DealerCode = "DIRECT";

            GenerateFanNoResponse generateFanNoResponse = await GenerateFanNo(generateFanNoRequest);
            FanNo = generateFanNoResponse.FanNo;
            //update fan no.
            UpdateFleetFanNoRequest updateFleetFanNoRequest = new UpdateFleetFanNoRequest();
            updateFleetFanNoRequest.FleetID = uploadDocumentsDMSRequest.FleetId;
            updateFleetFanNoRequest.FanNo = FanNo;
            UpdateFleetFanNoResponse updateFleetFanNoResponse = await _dMSRepository.UpdateFleetFanNo(updateFleetFanNoRequest);

        }
        if (dMSDocumentAndFleetResponseModel.DmsDocumentDetailsResponseModels?.Count > 0 && !string.IsNullOrEmpty(FanNo) && !string.IsNullOrEmpty(ApplicantName))
        {

            int totalDocCount = dMSDocumentAndFleetResponseModel.DmsDocumentDetailsResponseModels.Count;
            int successDocCount = 0;
            int failedDocCount = 0;
            bool rcuIsProcessed, dMSUploadStatus;
            string rcuComment, dMSComment;
            foreach (DmsDocumentDetailsResponseModel dmsDocumentDetailsResponseModel in dMSDocumentAndFleetResponseModel.DmsDocumentDetailsResponseModels)
            {
                string path = Path.Combine(_letterOptions.DocumentFolderPath, uploadDocumentsDMSRequest.FleetId.ToString());
                if (Directory.Exists(path))
                {
                    path = Path.Combine(path, dmsDocumentDetailsResponseModel.DocumentName + dmsDocumentDetailsResponseModel.Ext);
                }
                byte[] bytes = File.ReadAllBytes(path);
                string documentData = Convert.ToBase64String(bytes);
                UploadDocumentsDMSRequestModel uploadDocumentsDMSRequestModel = new UploadDocumentsDMSRequestModel();
                uploadDocumentsDMSRequestModel.Fanno = FanNo;
                uploadDocumentsDMSRequestModel.AppType = "1";
                uploadDocumentsDMSRequestModel.Binary = documentData;
                uploadDocumentsDMSRequestModel.DocumentName = Convert.ToString(dmsDocumentDetailsResponseModel.DocumentTypeName);
                uploadDocumentsDMSRequestModel.MimeType = "application/pdf";
                uploadDocumentsDMSRequestModel.ProcessType = uploadDocumentsDMSRequest.IsRCUUpload ? "RCUFL" : "NRCUFL";
                uploadDocumentsDMSRequestModel.ApplicantName = ApplicantName;
                UploadDocumentsDMSResponseModel uploadDocumentsDMSResponseModel = await _dMSRepository.UploadDocument(uploadDocumentsDMSRequestModel);
                if (uploadDocumentsDMSResponseModel.StatusCode == "CREATED")
                {
                    successDocCount++;
                    dMSUploadStatus = true;
                    dMSComment = uploadDocumentsDMSResponseModel.Message;
                }
                else
                {
                    failedDocCount++;
                    dMSUploadStatus = false;
                    dMSComment = uploadDocumentsDMSResponseModel.Message;
                }
                //update in document table
                UpdateDmsDocumentStatusRequestModel updateDmsDocumentStatusRequestModel = new UpdateDmsDocumentStatusRequestModel();
                updateDmsDocumentStatusRequestModel.DocumentID = dmsDocumentDetailsResponseModel.DocumentID;
                updateDmsDocumentStatusRequestModel.DMSUploadStatus = dMSUploadStatus;
                updateDmsDocumentStatusRequestModel.DMSComment = dMSComment;
                updateDmsDocumentStatusRequestModel.DMSDateTime = DateTime.Now;
                UpdateDmsDocumentStatusResponseModel updateDmsDocumentStatusResponseModel = await _dMSRepository.UpdateDmsDocumentStatus(updateDmsDocumentStatusRequestModel);
            }
            if (successDocCount > 0)
            {
                if (totalDocCount == successDocCount + failedDocCount)
                {
                    rcuIsProcessed = true;
                    rcuComment = " Documents Uploaded Successfully";
                }
                else
                {
                    rcuIsProcessed = false;
                    rcuComment = successDocCount + " Uploaded And " + failedDocCount + " Failed";
                }
            }
            else
            {
                rcuIsProcessed = false;
                rcuComment = "UploadS Failed";
            }
            //update in RCUUpdateQueue Table or DocumentUpdateQueue
            UpdateRcuRequestModel updateRcuRequestModel = new UpdateRcuRequestModel();
            updateRcuRequestModel.FleetId = uploadDocumentsDMSRequest.FleetId;
            updateRcuRequestModel.IsProcessed = rcuIsProcessed;
            updateRcuRequestModel.IsRCUUpload = uploadDocumentsDMSRequest.IsRCUUpload;
            updateRcuRequestModel.Comment = rcuComment;
            updateRcuRequestModel.ProcessedDate = DateTime.Now;
            updateRcuRequestModel.UpdatedDate = DateTime.Now;
            UpdateRcuResponseModel updateRcuResponseModel = await _dMSRepository.UpdateDocumentQueue(updateRcuRequestModel);
            if (updateRcuResponseModel.FleetId == 0)
            {
                uploadDocumentsDMSResponse.Message = "RCU Update Failed";
            }
            else
            {
                uploadDocumentsDMSResponse.Message = rcuComment;
            }
        }
        else
        {
            uploadDocumentsDMSResponse.Message = "Document Not Found For Uploading.";
        }
        return uploadDocumentsDMSResponse;
    }

    public async Task<ViewDocumentResponse> ViewDocument(ViewDocumentRequest viewDocumentRequest)
    {
        ViewDocumentResponse viewDocumentResponse = new ViewDocumentResponse();
        GetFanNoRequestModel getFanNoRequestModel=new GetFanNoRequestModel();
        getFanNoRequestModel.FleetId = viewDocumentRequest.FleetId;
        string FanNo = await _dMSRepository.GetFanNo(getFanNoRequestModel);
        if(!string.IsNullOrEmpty(FanNo))
        {
            //generate token
            GenerateTokenRequestModel generateTokenRequestModel = new GenerateTokenRequestModel();
            generateTokenRequestModel.DomainId = _dMSOptions.DomainId;
            GenerateTokenResponseModel generateTokenResponseModel = await _dMSRepository.GenerateToken(generateTokenRequestModel);

            ViewDocumentRequestModel viewDocumentRequestModel = new ViewDocumentRequestModel();           
            byte[] FanNoByte = System.Text.ASCIIEncoding.ASCII.GetBytes(FanNo);            
            viewDocumentRequestModel.Type = Convert.ToBase64String(FanNoByte);
            viewDocumentRequestModel.DomainId = generateTokenResponseModel.Token;
            ViewDocumentResponseModel viewDocumentResponseModel = await _dMSRepository.ViewDocument(viewDocumentRequestModel);
            if (!string.IsNullOrEmpty(viewDocumentResponseModel.DocumentHtml))
            {
                viewDocumentResponse.DocumentHtml = viewDocumentResponseModel.DocumentHtml;
            }           
        }
                     
        return viewDocumentResponse;
    }


}
