namespace Tmf.Saarthi.Infrastructure.Models.Request.Fleet;

public class AddDeviationStageRequestModel
{
    public long FleetId { get; set; }
    public string StageCode { get; set; } = string.Empty;
    public string VehicleIds { get; set; } = string.Empty;
    public long CreatedBy { get; set; }
    public string CreatedUserType { get; set;} = string.Empty;
    public DateTime CreatedDate { get; set; }
}