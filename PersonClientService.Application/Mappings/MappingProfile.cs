using AutoMapper;
using PersonClientService.Application.DTOs.Inputs;
using PersonClientService.Application.DTOs.Outputs;
using PersonClientService.Core.Domain.Entities;

namespace PersonClientService.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, CreateClientInputDto>().ReverseMap();
            CreateMap<Client, ClientOutputDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Person.Gender))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Person.DateOfBirth))
                .ForMember(dest => dest.Identification, opt => opt.MapFrom(src => src.Person.Identification))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Person.Address))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Person.Phone));
            CreateMap<Person, UpdateClientInputDto>().ReverseMap();
            CreateMap<Client, UpdateClientInputDto>().ReverseMap();
        }
    }
}
