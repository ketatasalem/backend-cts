using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleResponseDto>().ReverseMap(); ;
            CreateMap<ArticleUpdateDto, Article>().ReverseMap();
        }
    }
}
