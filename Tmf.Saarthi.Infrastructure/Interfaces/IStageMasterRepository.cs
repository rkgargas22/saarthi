using Tmf.Saarthi.Core.Enums;
using Tmf.Saarthi.Infrastructure.Models.Response.StageMaster;

namespace Tmf.Saarthi.Infrastructure.Interfaces
{
    public interface IStageMasterRepository
    {
        Task<StageMasterResponseModel> GetStageMasterByStageCode(StageCodeFlag stageCode);
    }
}
