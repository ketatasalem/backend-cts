using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class PosteChargeProfile : Profile
    {
        public PosteChargeProfile()
        {
            CreateMap<PostesDeCharge, PosteChargeResponseDto>().ReverseMap();
        }
    }
}
