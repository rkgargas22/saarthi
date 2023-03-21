namespace Tmf.Saarthi.Infrastructure.Models.Request.Agent;

public class SendToDeviationAgentRequestModel
{
    public long FleetID { get; set; }
    public string VehicleIds { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public long CreatedBy { get; set; }
    public string CreatedUserType { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
