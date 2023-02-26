namespace Tmf.Saarthi.Core.Options;

public class OcrOptions
{
    public const string OCR = "OCR";
    public string BaseUrl { get; set; } = string.Empty;
    public string ValidateDocument { get; set; } = string.Empty;
    public string AadharExtract { get; set; } = string.Empty;
    public string DLExtract { get; set; } = string.Empty;
    public string VoterIdExtract { get; set; } = string.Empty;
    public string DataMasking { get; set; } = string.Empty;
    public string TaskDetails { get; set; } = string.Empty;
}
