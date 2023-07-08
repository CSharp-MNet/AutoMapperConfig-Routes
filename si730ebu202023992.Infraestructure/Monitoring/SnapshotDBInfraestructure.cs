using Microsoft.EntityFrameworkCore;
using si730ebu202023992.Infraestructure.Context;
using si730ebu202023992.Infraestructure.Inventory.Model;
using si730ebu202023992.Infraestructure.Monitoring.Interface;
using si730ebu202023992.Infraestructure.Monitoring.Model;

namespace si730ebu202023992.Infraestructure.Monitoring;

public class SnapshotDBInfraestructure : ISnapshotInfraestructure
{
    private si730ebu202023992DBContext _si730Ebu202023992DbContext;
    
    public SnapshotDBInfraestructure(si730ebu202023992DBContext si730Ebu202023992DbContext)
    {
        _si730Ebu202023992DbContext = si730Ebu202023992DbContext;
    }
    
    public async Task<bool> SaveAsync(long productId, Snapshot snapshot)
    {
        try
        {
            var product = await _si730Ebu202023992DbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new ArgumentException("Product id not exists");
            }
            await _si730Ebu202023992DbContext.Snapshots.AddAsync(snapshot);
            await _si730Ebu202023992DbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<Snapshot>> GetAllSnapshots(long id)
    {
        var product = GetProductById(id);
        var snapshots = await _si730Ebu202023992DbContext.Snapshots.Where(s => s.ProductSerialNumber == product.SerialNumber).ToListAsync();
        return snapshots;
    }

    public Product GetProductById(long id)
    {
        var product = _si730Ebu202023992DbContext.Products.Find(id);
        if (product == null)
        {
            throw new ArgumentException("Product id not exists");
        }
        return product;
    }
    public bool ExistsBySnapshotId(long id)
    {
        var snapshot = _si730Ebu202023992DbContext.Snapshots.Find(id);
        if (snapshot == null)
        {
            return false;
        }
        return true;
    }
}