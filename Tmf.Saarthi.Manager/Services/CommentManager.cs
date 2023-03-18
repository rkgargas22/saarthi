using Tmf.Saarthi.Core.RequestModels.Comments;
using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.ResponseModels.Comments;
using Tmf.Saarthi.Core.ResponseModels.Document;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Request.Comments;
using Tmf.Saarthi.Infrastructure.Models.Request.Document;
using Tmf.Saarthi.Infrastructure.Models.Response.Comments;
using Tmf.Saarthi.Infrastructure.Models.Response.Document;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services
{
    public class CommentManager : ICommentManager
    {

        private readonly ICommentRepository _commentRepository;
        public CommentManager(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }


        public async Task<List<CommentsResponse>> GetComment(long fleetId, int stageId)
        {
            List<CommentsResponseModel> downloadDocumentResponseModelList = await _commentRepository.GetComment(fleetId, stageId);
            List<CommentsResponse> commentsResponses = new List<CommentsResponse>();

            foreach (CommentsResponseModel model in downloadDocumentResponseModelList)
            {
                CommentsResponse commentsResponse = new CommentsResponse();
                commentsResponse.CommentId = model.CommentId;
                commentsResponse.FleetId = model.FleetId;
                commentsResponse.StageId = model.StageId;
                commentsResponse.CommentDescription = model.CommentDescription;
                commentsResponse.AssignedTo = model.AssignedTo;
                commentsResponse.CreatedDate = model.CreatedDate;
                commentsResponses.Add(commentsResponse);
            }

            return commentsResponses;
        }

        public async Task<SaveCommentsResponse> SaveComments(SaveCommentsRequest saveCommentsRequest)
        {
            SaveCommentsRequestModel saveCommentsRequestModel = new SaveCommentsRequestModel();
            saveCommentsRequestModel.FleetId = saveCommentsRequest.FleetId;
            saveCommentsRequestModel.CommentDescription = saveCommentsRequest.CommentDescription;
            saveCommentsRequestModel.StageId = saveCommentsRequest.StageId;

            SaveCommentsResponseModel saveCommentsResponseModel = await _commentRepository.SaveComments(saveCommentsRequest);

            SaveCommentsResponse saveCommentsResponse = new SaveCommentsResponse();
            if (saveCommentsRequest.FleetId == 0)
            {
                saveCommentsResponse.Message = "Update Failed";
            }
            else
            {
                saveCommentsResponse.Message = "Updated Successfully";
            }
            return saveCommentsResponse;
        }
    }
}
