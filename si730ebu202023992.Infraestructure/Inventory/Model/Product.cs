using System.ComponentModel.DataAnnotations.Schema;
using si730ebu202023992.Infraestructure.Inventory.Enum;

namespace si730ebu202023992.Infraestructure.Inventory.Model;

public class Product
{
    [Column("id")]
    public long Id { get; set; }
    
    [Column("brand")]
    public string Brand { get; set; }

    [Column("model")]
    public string Model { get; set; }

    [Column("serial_number")]
    public string SerialNumber { get; set; }

    [Column("monitoring_level")]
    public MonitoringLevel MonitoringLevel { get; set; }
}