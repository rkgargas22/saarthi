using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Enums;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Response.DocumentTypeMstr;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services
{
    public class DocumentTypeMstrRepository : IDocumentTypeMstrRepository
    {
        private readonly ISqlUtility _sqlUtility;
        private readonly ConnectionStringsOptions _connectionStringsOptions;

        public DocumentTypeMstrRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions)
        {
            _sqlUtility = sqlUtility;
            _connectionStringsOptions = connectionStringsOptions.Value;
        }

        public async Task<DocumentTypeMstrResponseModel> GetDocumentTypeMstrByDocumentCode(DocumentCodeFlag documentCode)
        {
            List<SqlParameter> parameters = new()
            {
                 new SqlParameter("DocumentCode", documentCode.ToString())
            };

            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_GetDocumentTypeMstrByDocumentCode", parameters);

            DocumentTypeMstrResponseModel documentTypeMstrResponseModel = new();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    documentTypeMstrResponseModel.DocTypeId = dt.Rows[i]["DocTypeId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["DocTypeId"];
                    documentTypeMstrResponseModel.DocumentName = dt.Rows[i]["DocumentName"] == DBNull.Value ? "" : (string)dt.Rows[i]["DocumentName"];
                    documentTypeMstrResponseModel.Category = dt.Rows[i]["Category"] == DBNull.Value ? "" : (string)dt.Rows[i]["Category"];
                    documentTypeMstrResponseModel.RcuApplicable = dt.Rows[i]["RcuApplicable"] == DBNull.Value ? false : (bool)dt.Rows[i]["RcuApplicable"];
                    documentTypeMstrResponseModel.IsActive = dt.Rows[i]["IsActive"] == DBNull.Value ? false : (bool)dt.Rows[i]["IsActive"];
                    documentTypeMstrResponseModel.DocumentCode = dt.Rows[i]["DocumentCode"] == DBNull.Value ? "" : (string)dt.Rows[i]["DocumentCode"];
                }
            }

            return documentTypeMstrResponseModel;
        }
    }
}
