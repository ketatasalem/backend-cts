using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.Vues;

namespace Labo_Cts_backend.Application.Mappings
{
    public class BainReferenceAlternativeViewProfile : Profile
    {
        public BainReferenceAlternativeViewProfile()
        {
            CreateMap<BainReferenceAlternativeView, BainReferenceAlternativeViewResponseDto>().ReverseMap();
        }
    }
}
