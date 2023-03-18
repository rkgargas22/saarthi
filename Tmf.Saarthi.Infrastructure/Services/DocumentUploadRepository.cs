using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Document;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;
using Tmf.Saarthi.Infrastructure.Models.Response.Document;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services;

public class DocumentUploadRepository : IUploadDocumentRepository
{
    private readonly ISqlUtility _sqlUtility;
    private readonly IHttpService _httpService;
    private readonly ConnectionStringsOptions _connectionStringsOptions;

    public DocumentUploadRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, IHttpService httpService, IOptions<InstaVeritaOptions> instaVeritaOptions)
    {
        _sqlUtility = sqlUtility;
        _httpService = httpService;
        _connectionStringsOptions = connectionStringsOptions.Value;
    }

    public async Task<DocumentResponseModel> AddDocument(DocumentRequestModel documentRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("EntityCode", documentRequestModel.EntityCode),
            new SqlParameter("EntityId", documentRequestModel.EntityId),
            new SqlParameter("FleetID", documentRequestModel.FleetId),
            new SqlParameter("DocTypeId", documentRequestModel.DocTypeId),
            new SqlParameter("StageId", documentRequestModel.StageId),
            new SqlParameter("Ext", documentRequestModel.Extension),
            new SqlParameter("IsActive", documentRequestModel.IsActive),
            new SqlParameter("CreatedBy", documentRequestModel.CreatedBy),
            new SqlParameter("CreatedDate", documentRequestModel.CreatedDate),
            new SqlParameter("CreatedUserType", documentRequestModel.CreatedUserType),
            new SqlParameter("DocumentName", documentRequestModel.DocumentName)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_InsertDocumentByFleetID", parameters);

        DocumentResponseModel documentResponseModel = new DocumentResponseModel();
        if (dt.Rows.Count > 0)
        {
            documentResponseModel.DocumentId = (long)dt.Rows[0]["DocumentID"];
        }
        return documentResponseModel;
    }

    public async Task<UploadDocumentsResponseModel> UploadDocuments(UploadDocumentsRequestModel uploadDocumentsRequestModel)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("EntityCode", uploadDocumentsRequestModel.EntityCode),
            new SqlParameter("EntityId", uploadDocumentsRequestModel.EntityId),
            new SqlParameter("FleetId", uploadDocumentsRequestModel.FleetId),
            new SqlParameter("DocTypeId", uploadDocumentsRequestModel.DocTypeId),
            new SqlParameter("StageId", uploadDocumentsRequestModel.StageId),
            new SqlParameter("DocumentName", uploadDocumentsRequestModel.DocumentName),
            new SqlParameter("Extension", uploadDocumentsRequestModel.Extension),
            new SqlParameter("CreatedBy", uploadDocumentsRequestModel.CreatedBy),
            new SqlParameter("CreatedDate", uploadDocumentsRequestModel.CreatedDate),
            new SqlParameter("CreatedUserType", uploadDocumentsRequestModel.CreatedUserType)
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_uploadDocumentData", parameters);

        UploadDocumentsResponseModel uploadDocumentsResponseModel = new UploadDocumentsResponseModel();
        if (dt.Rows.Count > 0)
        {
            uploadDocumentsResponseModel.DocumentId = Convert.ToInt64(dt.Rows[0]["DocumentID"]);          
        }

        return uploadDocumentsResponseModel;
    }

    public async Task<List<DownloadDocumentResponseModel>> DownloadDocument(long fleetId, int stageId, int DocTypeId)
    {
        List<SqlParameter> parameters = new List<SqlParameter>()
            {
            new SqlParameter("FleetId", fleetId),
            new SqlParameter("StageId", stageId),
            new SqlParameter("DocTypeId", DocTypeId)
            };
        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetDocumentsTemp", parameters);


        List<DownloadDocumentResponseModel> downloadDocumentResponseModelList = new List<DownloadDocumentResponseModel>();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DownloadDocumentResponseModel downloadDocumentResponseModel = new DownloadDocumentResponseModel();
                downloadDocumentResponseModel.DocumentID = (Int64)dt.Rows[0]["DocumentID"];
                downloadDocumentResponseModel.FleetId = (Int64)dt.Rows[0]["FleetId"];
                downloadDocumentResponseModel.DocumentUrl = dt.Rows[0]["DocumentUrl"] == DBNull.Value ? string.Empty : (string)dt.Rows[0]["DocumentUrl"];
                downloadDocumentResponseModel.Documenttype = dt.Rows[0]["Documenttype"] == DBNull.Value ? string.Empty : (string)dt.Rows[0]["Documenttype"];
                downloadDocumentResponseModel.StageId = dt.Rows[0]["StageId"] == DBNull.Value ? 0 : (int)dt.Rows[0]["StageId"];
                downloadDocumentResponseModelList.Add(downloadDocumentResponseModel);
            }
        }
        return downloadDocumentResponseModelList;
    }

}
