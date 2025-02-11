using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class ParametresVersionProfile : Profile
    {
        public ParametresVersionProfile()
        {
            CreateMap<ParametresVersion, ParametreVersionCreateDto>().ReverseMap();
            CreateMap<ParametresVersion, ParametreVersionResponseDto>().ReverseMap();
            CreateMap<ParametreVersionCreateDto, ParametreVersionResponseDto>().ReverseMap();
        }
    }
}
