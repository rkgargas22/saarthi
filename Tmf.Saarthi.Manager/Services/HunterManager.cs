using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmf.Saarthi.Core.RequestModels.Hunter;
using Tmf.Saarthi.Core.ResponseModels.Hunter;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services
{
    public class HunterManager : IHunterManager
    {
        private readonly IHunterRepository _hunterRepository;

        public HunterManager(IHunterRepository hunterRepository)
        {
            _hunterRepository = hunterRepository;
        }

        public async Task<bool>  GetHunterResponse()
        {
           
          bool flag=  await _hunterRepository.HunterRequest();
            return flag;
           
        }
    }
}
