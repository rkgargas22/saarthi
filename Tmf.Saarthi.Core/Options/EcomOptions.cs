namespace Tmf.Saarthi.Core.Options
{
    public class EcomOptions
    {
        public const string Ecom = "Ecom";
       
        public string GenerateManifest { get; set; } = string.Empty;
        public string RescheduleOrCancelRequest { get; set; } = string.Empty;
        public Url BaseUrl { get; set; }
    }

    public class Url
    {
        public string GenerateManifestUrl { get; set; } = string.Empty;
        public string RescheduleOrCancelRequestUrl { get; set; } = string.Empty;
    }
}
