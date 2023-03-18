using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using Tmf.Saarthi.Core.Options;
using Tmf.Saarthi.Core.RequestModels.Comments;
using Tmf.Saarthi.Infrastructure.HttpService;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Document;
using Tmf.Saarthi.Infrastructure.Models.Response.Comments;
using Tmf.Saarthi.Infrastructure.Models.Response.Customer;
using Tmf.Saarthi.Infrastructure.Models.Response.Document;
using Tmf.Saarthi.Infrastructure.SqlService;

namespace Tmf.Saarthi.Infrastructure.Services
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ISqlUtility _sqlUtility;
        private readonly IHttpService _httpService;
        private readonly ConnectionStringsOptions _connectionStringsOptions;

        public CommentRepository(ISqlUtility sqlUtility, IOptions<ConnectionStringsOptions> connectionStringsOptions, IHttpService httpService, IOptions<InstaVeritaOptions> instaVeritaOptions)
        {
            _sqlUtility = sqlUtility;
            _httpService = httpService;
            _connectionStringsOptions = connectionStringsOptions.Value;
        }


        public async Task<List<CommentsResponseModel>> GetComment(long fleetId, int stageId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("FleetId", fleetId),
                new SqlParameter("StageId", stageId)
            };
            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_getComments", parameters);


            List<CommentsResponseModel> commentsResponseModelList = new List<CommentsResponseModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CommentsResponseModel commentsResponseModel = new CommentsResponseModel();
                    commentsResponseModel.CommentId = (Int64)dt.Rows[i]["CommentId"];
                    commentsResponseModel.FleetId = (Int64)dt.Rows[i]["FleetId"];
                    commentsResponseModel.CommentDescription = dt.Rows[i]["CommentDescription"] == DBNull.Value ? string.Empty : (string)dt.Rows[i]["CommentDescription"];
                    commentsResponseModel.StageId = dt.Rows[i]["StageId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["StageId"];
                    commentsResponseModel.AssignedTo = dt.Rows[i]["AssignedTo"] == DBNull.Value ? 0 : (Int64)dt.Rows[i]["AssignedTo"];
                    commentsResponseModel.CreatedDate = dt.Rows[i]["CreatedDate"] == DBNull.Value ? null : (DateTime)dt.Rows[i]["CreatedDate"];

                    commentsResponseModelList.Add(commentsResponseModel);
                }
            }
            return commentsResponseModelList;
        }

        public async Task<SaveCommentsResponseModel> SaveComments(SaveCommentsRequest saveCommentsRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
        {
            new SqlParameter("FleetId", saveCommentsRequest.FleetId),
            new SqlParameter("Desc", saveCommentsRequest.CommentDescription),
            new SqlParameter("StageId", saveCommentsRequest.StageId)
        };

            DataTable dt = await _sqlUtility.ExecuteCommandAsync(_connectionStringsOptions.DefaultConnection, "usp_InsertComments", parameters);

            SaveCommentsResponseModel saveCommentsResponseModel = new SaveCommentsResponseModel();
            if (dt.Rows.Count > 0)
            {
                saveCommentsResponseModel.FleetId = Convert.ToInt64(dt.Rows[0]["FleetID"]);
            }

            return saveCommentsResponseModel;
        }
    }
}
