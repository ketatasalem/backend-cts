using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Application.Services;
using Labo_Cts_backend.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labo_Cts_backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BainController(IBainService bainService) : ControllerBase
    {
        private readonly IBainService _bainService = bainService;

        /// <summary>
        /// Crée un nouveau bain.
        /// </summary>
        /// <param name="bainCreateDto">Les données pour créer un bain.</param>
        /// <returns>Le bain créé.</returns>
        [HttpPost]
        public async Task<ApiResponse<BainCreateDto>> CreateBain([FromBody] BainCreateDto bainCreateDto)
        {
            return await _bainService.CreateBainAsync(bainCreateDto);
        }

        /// <summary>
        /// Met à jour un bain existant.
        /// </summary>
        /// <param name="code">Le code du bain à mettre à jour.</param>
        /// <param name="bainUpdateDto">Les données de mise à jour du bain.</param>
        /// <returns>Le bain mis à jour.</returns>
        [HttpPut("{code}")]
        public async Task<ApiResponse<BainUpdateDto>> UpdateBain(int code, [FromBody] BainUpdateDto bainUpdateDto)
        {
            return await _bainService.UpdateBainAsync(code, bainUpdateDto);
        }

        /// <summary>
        /// Récupère un bain par son code.
        /// </summary>
        /// <param name="code">Le code du bain.</param>
        /// <returns>Le bain correspondant.</returns>
        [HttpGet("{code}")]
        public async Task<ApiResponse<BainResponseDto>> GetBainByCode(int code)
        {
            return await _bainService.GetBainByCodeAsync(code);
        }

        /// <summary>
        /// Récupère tous les bains.
        /// </summary>
        /// <returns>Une liste de tous les bains.</returns>
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<BainResponseDto>>> GetAllBains()
        {
            return await _bainService.GetAllBainsAsync();
        }

        /// <summary>
        /// Récupère une liste paginée de bains avec des filtres optionnels.
        /// </summary>
        /// <param name="query">Les paramètres de pagination et de filtrage.</param>
        /// <returns>Une réponse paginée contenant les bains correspondants.</returns>
        [HttpGet("paged")]
        public async Task<ApiResponse<PagedResult<BainResponseDto>>> GetPagedBains([FromQuery] FilterQuery query)
        {
            return await _bainService.GetPagedBainsAsync(query);
        }

        [HttpGet("referenceBainByCts/{code}")]
        public async Task<ApiResponse<IEnumerable<BainResponseDto>>> GetReferenceBainsByCTS(string code)
        {
            return await _bainService.GetReferenceBainsByCTSAsync(code);
        }
    }
}
