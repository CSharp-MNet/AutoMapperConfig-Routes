using AutoMapper;
using si730ebu202023992.API.Monitoring.Dto.Response;
using si730ebu202023992.Infraestructure.Monitoring.Model;

namespace si730ebu202023992.API.Monitoring.Mapper;

public class SnapshotToResponse : Profile
{
    public SnapshotToResponse()
    {
        CreateMap<Snapshot, SnapshotResponse>();
    }
}