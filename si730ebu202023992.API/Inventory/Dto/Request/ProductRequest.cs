namespace si730ebu202023992.API.Inventory.Dto.Request;

public class ProductRequest
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public string MonitoringLevel { get; set; }
}