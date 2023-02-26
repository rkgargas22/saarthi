using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Document;
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
            new SqlParameter("FleetID", documentRequestModel.FleetId),
            new SqlParameter("DocumentUrl", documentRequestModel.DocumentUrl),
            new SqlParameter("CreatedBy", documentRequestModel.CreatedBy),
            new SqlParameter("Documenttype", documentRequestModel.Documenttype),
            new SqlParameter("IsActive", documentRequestModel.IsActive),
            
        };

        DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_InsertDocumentByFleetID", parameters);

        DocumentResponseModel documentResponseModel = new DocumentResponseModel();
        if (dt.Rows.Count > 0)
        {
            documentResponseModel.FleetID = (long)dt.Rows[0]["FleetID"];
        }
        return documentResponseModel;
    }

}
