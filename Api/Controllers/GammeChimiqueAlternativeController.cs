using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Application.Services;
using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo_Cts_backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GammeChimiqueAlternativeController(IGammesChimiquesAlternativeService gammesChimiquesAlternativeService) : ControllerBase
    {
        private readonly IGammesChimiquesAlternativeService _gammesChimiquesAlternativeService = gammesChimiquesAlternativeService;

        [HttpGet("ByCodeBain/{code}")]
        public async Task<ApiResponse<IEnumerable<AlternativeResponseDto>>> GetAllAlternativesOfReferenceBain(int code)
        {
            return await _gammesChimiquesAlternativeService.GetAllAlternativesOfReferenceBainAsync(code);
        }

        [HttpGet("ByCodeBain/{code}/withLastVersion")]
        public async Task<ApiResponse<IEnumerable<AlternativeLastVersionResponseDto>>> GetAllAlternativesOfReferenceBainWithLastVersion(int code)
        {
            return await _gammesChimiquesAlternativeService.GetAllAlternativesOfReferenceBainWithLastVersionAsync(code);
        }

        /// <summary>
        /// Récupère une liste paginée des alternatives avec des filtres optionnels.
        /// </summary>
        /// <param name="query">Les paramètres de pagination et de filtrage.</param>
        /// <returns>Une réponse paginée contenant les alternatives correspondantes.</returns>
        [HttpGet("paged")]
        public async Task<ApiResponse<PagedResult<BainReferenceAlternativeViewResponseDto>>> GetPagedAllAlternatives([FromQuery] FilterQuery query)
        {
            return await _gammesChimiquesAlternativeService.GetPagedAllAlternativesAsync(query);
        }

        /// <summary>
        /// Récupère une alternative par son id.
        /// </summary>
        /// <param name="id">L'id d'alternative.</param>
        /// <returns>L'alternative correspondante.</returns>
        [HttpGet("{id}")]
        public async Task<ApiResponse<AlternativeResponseDto>> GetBainByCode(int id)
        {
            return await _gammesChimiquesAlternativeService.GetAlternativeByCodeAsync(id);
        }

        [HttpPost]
        public async Task<ApiResponse<AlternativeCreateDto>> CreateAlternative([FromBody] AlternativeCreateDto alternativeCreateDto)
        {
            return await _gammesChimiquesAlternativeService.CreateAlternativeAsync(alternativeCreateDto);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<AlternativeResponseDto>> UpdateVersionsOfAlternative(int id, [FromBody] GammeChimiqueAlternativeUpdateDto alternativeUpdate)
        {
            return await _gammesChimiquesAlternativeService.UpdateAlternativeAsync(id, alternativeUpdate);
        }
    }
}
