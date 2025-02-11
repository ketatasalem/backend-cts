using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class RatioArticleProfile : Profile
    {
        public RatioArticleProfile()
        {
            CreateMap<RatioArticle, RatioArticleCreateDto>().ReverseMap();
            CreateMap<RatioArticle, RatioArticleResponseDto>().ReverseMap();
            CreateMap<RatioArticleCreateDto, RatioArticleResponseDto>().ReverseMap();
        }
    }
}
