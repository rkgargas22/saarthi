using Tmf.Saarthi.Core.RequestModels.Comments;
using Tmf.Saarthi.Core.RequestModels.Document;
using Tmf.Saarthi.Core.ResponseModels.Comments;
using Tmf.Saarthi.Core.ResponseModels.Document;

namespace Tmf.Saarthi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentManager _commentManager;
        // private readonly IValidator<DocumentRequest> _documentRequestValidator;

        public CommentController(ICommentManager commentManager)//, IValidator<DocumentRequest> documentRequestValidator)
        {
            _commentManager = commentManager;
        }

        [HttpGet("Comment/{FleetId}/{StageId}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<CommentsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComment([FromRoute] long FleetId, [FromRoute] int StageId)
        {
            List<CommentsResponse> commentResponse = await _commentManager.GetComment(FleetId, StageId);
            return Ok(commentResponse);
        }

        [HttpPost]
        [Route("SaveComments")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SaveCommentsResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> SaveComments([FromBody] SaveCommentsRequest saveCommentsRequest)
        {
            SaveCommentsResponse uploadDocumentsResponse = await _commentManager.SaveComments(saveCommentsRequest);

            return Ok(uploadDocumentsResponse);
        }
    }
}
