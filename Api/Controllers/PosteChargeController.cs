using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Application.Services;
using Labo_Cts_backend.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo_Cts_backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosteChargeController(IPosteChargeService posteChargeService) : ControllerBase
    {
        private readonly IPosteChargeService _posteChargeService = posteChargeService;

        /// <summary>
        /// Récupère toutes les postes de charge.
        /// </summary>
        /// <returns>Une liste de toutes les postes de charge.</returns>
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<PosteChargeResponseDto>>> GetAllPosteCharge()
        {
            return await _posteChargeService.GetAllPosteChargeAsync();
        }

        /// <summary>
        /// Récupère une liste paginée de postes de charge avec des filtres optionnels.
        /// </summary>
        /// <param name="query">Les paramètres de pagination et de filtrage.</param>
        /// <returns>Une réponse paginée contenant les postes de charge correspondants.</returns>
        [HttpGet("paged")]
        public async Task<ApiResponse<PagedResult<PosteChargeResponseDto>>> GetPagedPosteCharge([FromQuery] FilterQuery query)
        {
            return await _posteChargeService.GetPagedPosteChargeAsync(query);
        }

        /// <summary>
        /// Récupère une poste de charge par son code.
        /// </summary>
        /// <param name="code">Le code du poste de charge.</param>
        /// <returns>La poste de charge correspondante.</returns>
        [HttpGet("{code}")]
        public async Task<ApiResponse<PosteChargeResponseDto>> GetPosteChargeByCode(String code)
        {
            return await _posteChargeService.GetPosteChargeByCodeAsync(code);
        }

    }
}
