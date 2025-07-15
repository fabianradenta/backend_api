namespace TelemetryApi.Models;

public class TelemetryData {
    public string? device_id { get; set; }
    public string? timestamp { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public double speed { get; set; }
    public double fuel { get; set; }
    public double engine_hours { get; set; }
    public bool ignition { get; set; }
}