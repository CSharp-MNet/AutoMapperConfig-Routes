using si730ebu202023992.Infraestructure.Context;
using si730ebu202023992.Infraestructure.Inventory.Interface;
using si730ebu202023992.Infraestructure.Inventory.Model;

namespace si730ebu202023992.Infraestructure.Inventory;

public class ProductDBInfraestructure : IProductInfraestructure
{
    private si730ebu202023992DBContext _si730Ebu202023992DbContext;
    
    public ProductDBInfraestructure(si730ebu202023992DBContext si730Ebu202023992DbContext)
    {
        _si730Ebu202023992DbContext = si730Ebu202023992DbContext;
    }
    
    public async Task<bool> SaveAsync(Product product)
    {
        try
        {
            await _si730Ebu202023992DbContext.Products.AddAsync(product);
            await _si730Ebu202023992DbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Product Get(long id)
    {
        var product = _si730Ebu202023992DbContext.Products.Find(id);
        if (product == null)
        {
            throw new ArgumentException("Product with id " + id + " not found");
        }
        return product;
    }

    public bool ExistsBySerialNumber(string serialNumber)
    {
        var bySerialNumber = _si730Ebu202023992DbContext.Products.Where(p => p.SerialNumber == serialNumber).FirstOrDefault();
        if (bySerialNumber == null)
        {
            return false;
        }
        return true;
    }
}