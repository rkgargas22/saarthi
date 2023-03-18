using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Core.ResponseModels.Security
{
    public class TokenClaims
    {
       public string BPNumber { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string Role { get; set; }
    }
}
