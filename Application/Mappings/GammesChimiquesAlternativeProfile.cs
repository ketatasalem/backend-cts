using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class GammesChimiquesAlternativeProfile : Profile
    {
        public GammesChimiquesAlternativeProfile()
        {
            CreateMap<GammesChimiquesAlternative, AlternativeCreateDto>().ReverseMap();
            CreateMap<GammesChimiquesAlternative, AlternativeUpdateDto>().ReverseMap();
            CreateMap<AlternativeCreateDto, AlternativeUpdateDto>().ReverseMap();
            CreateMap<GammesChimiquesAlternative, AlternativeResponseDto>().ReverseMap();
            CreateMap<AlternativeCreateDto, AlternativeResponseDto>().ReverseMap();
            CreateMap<AlternativeUpdateDto, AlternativeResponseDto>().ReverseMap();
            CreateMap<GammesChimiquesAlternative,   AlternativeLastVersionResponseDto>().ReverseMap();
            CreateMap<GammesChimiquesAlternative,   GammeChimiqueAlternativeUpdateDto>().ReverseMap();

        }
    }
}
