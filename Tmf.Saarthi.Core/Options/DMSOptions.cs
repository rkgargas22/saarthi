namespace Tmf.Saarthi.Core.Options;

public class DMSOptions
{
    public const string DMS = "DMS";
    public string BaseUrl { get; set; } = string.Empty;
    public string GenerateFanNo { get; set; } = string.Empty;
    public string GenerateToken { get; set; } = string.Empty;
    public string DomainId { get; set; } = string.Empty;
    public string UploadDocument { get; set; }=string.Empty;
    public string ViewDocument { get; set; } = string.Empty;

}
