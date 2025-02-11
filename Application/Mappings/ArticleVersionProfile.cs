using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class ArticleVersionProfile : Profile
    {
        public ArticleVersionProfile()
        {
            CreateMap<ArticlesVersion, ArticleVersionCreateDto>().ReverseMap();
            CreateMap<ArticlesVersion, ArticleVersionResponseDto>().ReverseMap();
            CreateMap<ArticleVersionCreateDto, ArticleVersionResponseDto>().ReverseMap();
        }
    }
}
