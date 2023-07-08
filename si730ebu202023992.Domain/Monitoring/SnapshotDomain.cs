using si730ebu202023992.Domain.Monitoring.Interface;
using si730ebu202023992.Infraestructure.Inventory.Enum;
using si730ebu202023992.Infraestructure.Monitoring.Interface;
using si730ebu202023992.Infraestructure.Monitoring.Model;

namespace si730ebu202023992.Domain.Monitoring;

public class SnapshotDomain : ISnapshotDomain
{
    private ISnapshotInfraestructure _snapshotInfraestructure;
    
    public SnapshotDomain(ISnapshotInfraestructure snapshotInfraestructure)
    {
        _snapshotInfraestructure = snapshotInfraestructure;
    }
    
    public Task<bool> SaveAsync(long productId, Snapshot snapshot)
    {
        ValidateSnapshot(snapshot);
        ValidateRequestEnergyAndLeakage(productId, snapshot);
        ValidateSnapshotIdAndProductSerialNumber(snapshot, productId);
        return _snapshotInfraestructure.SaveAsync(productId, snapshot);
    }

    public Task<List<Snapshot>> GetAllSnapshotsAsync(long id)
    {
        return _snapshotInfraestructure.GetAllSnapshots(id);
    }
    
    private void ValidateSnapshot(Snapshot snapshot)
    {
        if (string.IsNullOrWhiteSpace(snapshot.SnapshotId))
        {
            throw new ArgumentException("SnapshotId is required");
        }
        if (string.IsNullOrWhiteSpace(snapshot.ProductSerialNumber))
        {
            throw new ArgumentException("ProductSerialNumber is required");
        }
        if (string.IsNullOrWhiteSpace(snapshot.ProductSerialNumber))
        {
            throw new ArgumentException("ProductSerialNumber is required");
        }
    }

    private void ValidateRequestEnergyAndLeakage(long productId, Snapshot snapshot)
    {
        var product = _snapshotInfraestructure.GetProductById(productId);
        if (product.MonitoringLevel == MonitoringLevel.AdvanceMonitoring)
        {
            if (snapshot.Energy <= 0)
            {
                throw new ArgumentException("Snapshot energy cannot be empty and must be greater than 0");
            }
            if (double.IsNegative(snapshot.Leakage) || snapshot.Leakage >= 2)
            {
                throw new ArgumentException("Snapshot leakage cannot be empty and must be between 0 = No Leakage, 1 = Leakage");
            }
        }
        else if (product.MonitoringLevel == MonitoringLevel.EssentialMonitoring)
        {
            if (snapshot.Energy > 0 || !double.IsNegative(snapshot.Leakage) || snapshot.Leakage < 2)
            { 
                throw new ArgumentException("Snapshot Data not compatible with product current Monitoring Level");
            }
        }
    }
    private void ValidateSnapshotIdAndProductSerialNumber(Snapshot snapshot, long productId)
    {
        if (_snapshotInfraestructure.ExistsBySnapshotId(snapshot.Id))
        {
            throw new ArgumentException("SnapshotId already exists. SnapshotId must be unique");
        }
        var product = _snapshotInfraestructure.GetProductById(productId);
        if (!snapshot.ProductSerialNumber.Equals(product.SerialNumber))
        {
            throw new ArgumentException("Incorrect. The product id corresponds to the serial number → " + product.SerialNumber);
        }
    }
}