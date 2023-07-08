using si730ebu202023992.Domain.Inventory.Interface;
using si730ebu202023992.Infraestructure.Inventory.Interface;
using si730ebu202023992.Infraestructure.Inventory.Model;

namespace si730ebu202023992.Domain.Inventory;

public class ProductDomain : IProductDomain
{
    private IProductInfraestructure _productInfraestructure;
    
    public ProductDomain(IProductInfraestructure productInfraestructure)
    {
        _productInfraestructure = productInfraestructure;
    }
    
    public Task<bool> SaveAsync(Product product)
    {
        ValidateProduct(product);
        return _productInfraestructure.SaveAsync(product);
    }
    
    public Product GetProductById(long id)
    {
        if (IsValidId(id)) return _productInfraestructure.Get(id);
        throw new ArgumentException("Id must be greater than 0");
    }

    private bool IsValidId(long id)
    {
        if (id <= 0) return false;
        return true;
    }
    
    public void CheckMonitoringLevel(String monitoringLevel)
    {
        if (string.IsNullOrWhiteSpace(monitoringLevel))
        {
            throw new Exception("MonitoringLevel is required");
        }
        if (!monitoringLevel.Equals("EssentialMonitoring") && !monitoringLevel.Equals("AdvanceMonitoring"))
        {
            throw new Exception("MonitoringLevel is invalid must be EssentialMonitoring or AdvanceMonitoring");
        }
    }

    private void ValidateProduct(Product product)
    {
        if (_productInfraestructure.ExistsBySerialNumber(product.SerialNumber))
        {
            throw new Exception("Product already exists. SerialNumber must be unique");
        }
        if (string.IsNullOrWhiteSpace(product.Brand))
        {
            throw new Exception("Brand is required");
        }
        if (string.IsNullOrWhiteSpace(product.Model))
        {
            throw new Exception("Model is required");
        }
        if (string.IsNullOrWhiteSpace(product.SerialNumber))
        {
            throw new Exception("SerialNumber is required");
        }
    }
}