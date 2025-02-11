using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class BainProfile : Profile
    {
        public BainProfile()
        {
            CreateMap<Bain, BainCreateDto>().ReverseMap();
            CreateMap<Bain, BainResponseDto>().ReverseMap();
            CreateMap<BainCreateDto, BainResponseDto>().ReverseMap();
            CreateMap<Bain, BainUpdateDto>().ReverseMap();
            CreateMap<BainResponseDto, BainUpdateDto>().ReverseMap();
        }
    }
}
