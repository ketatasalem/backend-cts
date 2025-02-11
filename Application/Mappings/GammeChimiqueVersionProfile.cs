using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class GammeChimiqueVersionProfile : Profile
    {
        public GammeChimiqueVersionProfile()
        {
            CreateMap<GammesChimiquesVersion, GammeChimiqueVersionCreateDto>().ReverseMap();
        }
    }
}
