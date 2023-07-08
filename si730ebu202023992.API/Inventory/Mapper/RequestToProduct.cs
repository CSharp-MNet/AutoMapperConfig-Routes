using AutoMapper;
using si730ebu202023992.API.Inventory.Dto.Request;
using si730ebu202023992.Infraestructure.Inventory.Enum;
using si730ebu202023992.Infraestructure.Inventory.Model;

namespace si730ebu202023992.API.Inventory.Mapper;

public class RequestToProduct : Profile
{
    public RequestToProduct()
    {
        CreateMap<ProductRequest, Product>()
            .ForMember(dest => dest.MonitoringLevel,
                opt => 
                    opt.MapFrom(src => 
                        Enum.Parse(typeof(MonitoringLevel), src.MonitoringLevel)
                        )
                    );
    }
}