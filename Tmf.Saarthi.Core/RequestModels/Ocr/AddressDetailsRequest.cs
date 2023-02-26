namespace Tmf.Saarthi.Core.RequestModels.Ocr;

public class AddressDetailsRequest
{
    public long BpNo { get; set; }
    public long FleetID { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FrontPage { get; set; } = string.Empty;
    public string BackPage { get; set; } = string.Empty;

}
