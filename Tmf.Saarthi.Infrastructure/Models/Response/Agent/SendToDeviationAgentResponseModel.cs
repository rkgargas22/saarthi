namespace Tmf.Saarthi.Infrastructure.Models.Response.Agent;

public class SendToDeviationAgentResponseModel
{
    public long FleetID { get; set; }
    public string Message { get; set; } = string.Empty;
    public long StageId { get; set; }
}
