using Tmf.Saarthi.Core.Enums;
using Tmf.Saarthi.Infrastructure.Models.Response.StageMaster;

namespace Tmf.Saarthi.Manager.Interfaces
{
    public interface IStageMasterManager
    {
        Task<StageMasterResponseModel> GetStageMasterByStageCode(StageCodeFlag stageCode);
    }
}
