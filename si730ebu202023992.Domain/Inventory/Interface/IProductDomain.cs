using si730ebu202023992.Infraestructure.Inventory.Model;

namespace si730ebu202023992.Domain.Inventory.Interface;

public interface IProductDomain
{
    public Task<bool> SaveAsync(Product product);
    public Product GetProductById(long id);
    public void CheckMonitoringLevel(String monitoringLevel);
}