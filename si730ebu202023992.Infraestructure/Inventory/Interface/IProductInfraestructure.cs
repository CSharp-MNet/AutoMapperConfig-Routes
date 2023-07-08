using si730ebu202023992.Infraestructure.Inventory.Model;

namespace si730ebu202023992.Infraestructure.Inventory.Interface;

public interface IProductInfraestructure
{
    public Task<bool> SaveAsync(Product product);
    public Product Get(long id);
    public bool ExistsBySerialNumber(string serialNumber);
}