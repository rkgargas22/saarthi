using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tmf.Saarthi.Infrastructure.Models.Response.DMS
{
    public class DmsDocumentDetailsResponseModel
    {
        [JsonPropertyName("documentID")]
        public long DocumentID { get; set; }

        [JsonPropertyName("DocumentName")]
        public string DocumentName { get; set; } = string.Empty;

        [JsonPropertyName("ext")]
        public string Ext { get; set; } = string.Empty;

        [JsonPropertyName("documentTypeName")]
        public string DocumentTypeName { get; set; } = string.Empty;


    }

    public class DmsFleetDetailResponseModel
    {
        [JsonPropertyName("FanNo")]
        public string FanNo { get; set; } = string.Empty;

        [JsonPropertyName("branchCode")]
        public double BranchCode { get; set; }

        [JsonPropertyName("ApplicantName")]
        public string ApplicantName { get; set; } = string.Empty;

        [JsonPropertyName("isAddressChanged")]
        public bool IsAddressChanged { get; set; }
    }

    public class DMSDocumentAndFleetResponseModel
    {
        public List<DmsDocumentDetailsResponseModel> DmsDocumentDetailsResponseModels { get; set; } = new List<DmsDocumentDetailsResponseModel>();

        public DmsFleetDetailResponseModel DmsFleetDetailResponseModel { get; set; } = new DmsFleetDetailResponseModel();
        
    }
}
