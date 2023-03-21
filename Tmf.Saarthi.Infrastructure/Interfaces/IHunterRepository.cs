using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmf.Saarthi.Core.RequestModels.Hunter;
using Tmf.Saarthi.Core.ResponseModels.Hunter;

namespace Tmf.Saarthi.Infrastructure.Interfaces
{
    public interface IHunterRepository
    {
        Task<HunterResponseModel> HunterVerification(HunterRequestModel requestModel);
        Task<bool> HunterRequest();
    }
}
