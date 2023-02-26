namespace Tmf.Saarthi.Core.Options;

public class FleetConfigurationOptions
{
    public const string FleetConfiguration = "FleetConfiguration";
    public int VehicleLimit { get; set; }
    public string VehicleType { get; set; } = string.Empty;
    public int PerVehicleSanction { get; set; }
    public int VehicleAgeCriteria { get; set; }
    public decimal ProcessingFee { get; set; }
    public decimal StampDuty { get; set; }
    public decimal IRR { get; set; }
    public decimal AIR { get; set; }
}
