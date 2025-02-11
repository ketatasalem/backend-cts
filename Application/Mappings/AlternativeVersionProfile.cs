using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class AlternativeVersionProfile : Profile
    {
        public AlternativeVersionProfile()
        {
            CreateMap<GammesChimiquesVersion, AlternativeVersionResponseDto>().ReverseMap(); 
        }
    }
}
