using System.Text.Json.Serialization;


namespace Tmf.Saarthi.Core.RequestModels.DMS
{
    public class UploadDocumentsDMSRequest
    {
        [JsonPropertyName("fleetId")]
        public long FleetId { get; set; } 

        [JsonPropertyName("isRCUUpload")]
        public Boolean IsRCUUpload { get; set; } 
    }
}
