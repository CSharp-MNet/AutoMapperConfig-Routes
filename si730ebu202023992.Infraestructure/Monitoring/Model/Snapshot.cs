using System.ComponentModel.DataAnnotations.Schema;

namespace si730ebu202023992.Infraestructure.Monitoring.Model;

public class Snapshot
{
    [Column("id")]
    public long Id { get; set; }
    
    [Column("snapshot_id")]
    public string SnapshotId { get; set; }
    
    [Column("product_serial_number")]
    public string ProductSerialNumber { get; set; }
    
    [Column("temperature")]
    public double Temperature { get; set; }
    
    [Column("energy")]
    public double Energy { get; set; }
    
    [Column("leakage")]
    public int Leakage { get; set; }
}