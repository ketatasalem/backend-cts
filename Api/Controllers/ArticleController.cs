using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labo_Cts_backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController(IArticleService articleService) : ControllerBase
    {
        private readonly IArticleService _articleService = articleService;

        /// <summary>
        /// Récupère tous les articles.
        /// </summary>
        /// <returns>Une liste d'articles.</returns>
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<ArticleResponseDto>>> GetAllArticles()
        {
            return await _articleService.GetAllArticlesAsync();
        }

        /// <summary>
        /// Récupère un article par son code.
        /// </summary>
        /// <param name="code">Le code de l'article.</param>
        /// <returns>L'article correspondant.</returns>
        [HttpGet("{code}")]
        public async Task<ApiResponse<ArticleResponseDto>> GetArticleByCode(string code)
        {
            return await _articleService.GetArticleByCodeAsync(code);
        }

        /// <summary>
        /// Met à jour un article existant.
        /// </summary>
        /// <param name="code">Le code de l'article à mettre à jour.</param>
        /// <param name="articleUpdateDto">Les données de mise à jour.</param>
        /// <returns>Le résultat de la mise à jour.</returns>
        [HttpPut("{code}")]
        public async Task<ApiResponse<ArticleUpdateDto>> UpdateArticle(string code, [FromBody] ArticleUpdateDto articleUpdateDto)
        {
            return await _articleService.UpdateArticleAsync(code, articleUpdateDto);
        }

        /// <summary>
        /// Récupère une liste paginée d'articles avec des filtres optionnels.
        /// </summary>
        /// <param name="query">Les paramètres de pagination et de filtrage.</param>
        /// <returns>Une réponse paginée contenant les articles correspondants.</returns>
        [HttpGet("paged")]
        public async Task<ApiResponse<PagedResult<ArticleResponseDto>>> GetPagedArticles([FromQuery] FilterQuery query)
        {
            return await _articleService.GetPagedArticlesAsync(query);
        }

        /// <summary>
        /// Récupère les ratios article par le code article.
        /// </summary>
        /// <param name="code">Le code de l'article.</param>
        /// <returns>Les ratios d'un correspondant.</returns>
        [HttpGet("getRatioByCodeArticle/{code}")]
        public async Task<ApiResponse<IEnumerable<RatioArticleResponseDto>>> GetRatioArticlesByCodeArticle(string code)
        {
            return await _articleService.GetRatioArticlesByCodeArticleAsync(code);
        }
    }
}
