using AutoMapper;
using si730ebu202023992.API.Monitoring.Dto.Request;
using si730ebu202023992.Infraestructure.Monitoring.Model;

namespace si730ebu202023992.API.Monitoring.Mapper;

public class RequestToSnapshot : Profile
{
    public RequestToSnapshot()
    {
        CreateMap<SnapshotRequest, Snapshot>();
    }
}