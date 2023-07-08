namespace si730ebu202023992.API.Inventory.Dto.Response;

public class ProductResponse
{
    public long Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public string MonitoringLevel { get; set; }
}