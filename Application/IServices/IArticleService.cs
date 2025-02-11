using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Application.IServices
{
    public interface IArticleService
    {
        Task<ApiResponse<IEnumerable<ArticleResponseDto>>> GetAllArticlesAsync();
        Task<ApiResponse<ArticleResponseDto>> GetArticleByCodeAsync(string code);
        Task<ApiResponse<ArticleUpdateDto>> UpdateArticleAsync(string code, ArticleUpdateDto dto);
        Task<ApiResponse<PagedResult<ArticleResponseDto>>> GetPagedArticlesAsync(FilterQuery query);
        Task<ApiResponse<IEnumerable<RatioArticleResponseDto>>> GetRatioArticlesByCodeArticleAsync(string code);
    }
}
