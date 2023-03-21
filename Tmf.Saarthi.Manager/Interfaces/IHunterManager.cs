using Tmf.Saarthi.Core.RequestModels.Hunter;
using Tmf.Saarthi.Core.ResponseModels.Hunter;

namespace Tmf.Saarthi.Manager.Interfaces
{
    public interface IHunterManager
    {
        Task<bool> GetHunterResponse();
        
    }
}
