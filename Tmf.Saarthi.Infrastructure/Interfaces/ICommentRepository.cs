using Tmf.Saarthi.Core.RequestModels.Comments;
using Tmf.Saarthi.Infrastructure.Models.Response.Comments;

namespace Tmf.Saarthi.Infrastructure.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentsResponseModel>> GetComment(long fleetId, int stageId);
        Task<SaveCommentsResponseModel> SaveComments(SaveCommentsRequest saveCommentsRequest);
    }
}
