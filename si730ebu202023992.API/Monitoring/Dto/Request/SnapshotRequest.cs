namespace si730ebu202023992.API.Monitoring.Dto.Request;

public class SnapshotRequest
{
    public string SnapshotId { get; set; }
    public string ProductSerialNumber { get; set; }
    public double Temperature { get; set; }
    public double Energy { get; set; }
    public int Leakage { get; set; }
}