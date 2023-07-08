using si730ebu202023992.Infraestructure.Inventory.Model;
using si730ebu202023992.Infraestructure.Monitoring.Model;

namespace si730ebu202023992.Infraestructure.Monitoring.Interface;

public interface ISnapshotInfraestructure
{
    public Task<bool> SaveAsync(long productId, Snapshot snapshot);
    public Task<List<Snapshot>> GetAllSnapshots(long id);
    public Product GetProductById(long id);
    public bool ExistsBySnapshotId(long id);
}