namespace Tmf.Saarthi.Core.RequestModels.Ocr;

public class AddressDetailsRequest
{
    public long BpNo { get; set; }
    public long FleetID { get; set; }
    public long StagId { get; set; }
    public int DocumentType { get; set; } = 0;
    public string DocumentExtension { get; set; } = string.Empty;
    public string FrontPage { get; set; } = string.Empty;
    public string BackPage { get; set; } = string.Empty;

}
