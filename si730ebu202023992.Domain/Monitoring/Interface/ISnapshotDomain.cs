using si730ebu202023992.Infraestructure.Monitoring.Model;

namespace si730ebu202023992.Domain.Monitoring.Interface;

public interface ISnapshotDomain
{
    public Task<bool> SaveAsync(long productId, Snapshot snapshot);
    public Task<List<Snapshot>> GetAllSnapshotsAsync(long id);
}