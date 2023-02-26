using Tmf.Saarthi.Core.RequestModels.DMS;
using Tmf.Saarthi.Core.ResponseModels.DMS;

namespace Tmf.Saarthi.Manager.Interfaces;

public interface IDMSManager
{
    Task<GenerateFanNoResponse> GenerateFanNo(GenerateFanNoRequest generateFanNoRequest);
}
