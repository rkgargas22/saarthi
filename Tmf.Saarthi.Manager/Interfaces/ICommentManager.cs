using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmf.Saarthi.Core.RequestModels.Comments;
using Tmf.Saarthi.Core.ResponseModels.Comments;

namespace Tmf.Saarthi.Manager.Interfaces
{
    public interface ICommentManager
    {
        Task<List<CommentsResponse>> GetComment(long fleetId, int stageId);
        Task<SaveCommentsResponse> SaveComments(SaveCommentsRequest uploadDocumentsRequest);
    }
}
