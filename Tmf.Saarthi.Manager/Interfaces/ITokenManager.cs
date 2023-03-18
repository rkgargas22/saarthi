using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tmf.Saarthi.Core.ResponseModels.Security;
using Tmf.Saarthi.Manager.Services;

namespace Tmf.Saarthi.Manager.Interfaces
{
    public interface ITokenManager
    {
         string AccesToken(TokenClaims claimPram);
        
    }
}
