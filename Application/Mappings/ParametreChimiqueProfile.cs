using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class ParametreChimiqueProfile : Profile
    {
        public ParametreChimiqueProfile() 
        {
            CreateMap<ParametresChimique, ParametreChimiqueCreateDto>().ReverseMap();
            CreateMap<ParametresChimique, ParametreChimiqueResponseDto>().ReverseMap();
            CreateMap<ParametreChimiqueCreateDto, ParametreChimiqueResponseDto>().ReverseMap();
        }
    }
}
