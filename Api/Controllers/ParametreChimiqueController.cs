using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo_Cts_backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametreChimiqueController(IParametreChimiqueService parametreChimiqueService) : ControllerBase
    {
        private readonly IParametreChimiqueService _parametreChimiqueService = parametreChimiqueService;

        /// <summary>
        /// Récupère tous les paramètres chimiques.
        /// </summary>
        /// <returns>Une liste des paramètres chimiques.</returns>
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<ParametreChimiqueResponseDto>>> GetAllParametersChimique()
        {
            return await _parametreChimiqueService.GetAllParametersChimiqueAsync();
        }

        /// <summary>
        /// Récupère un paramètre chimique par son code.
        /// </summary>
        /// <param name="code">Le code de paramètre chimique.</param>
        /// <returns>Le paramètre chimique correspondant</returns>
        [HttpGet("{code}")]
        public async Task<ApiResponse<ParametreChimiqueResponseDto>> GetAllParametersChimique(string code)
        {
            return await _parametreChimiqueService.GetParameterChimiqueByCodeAsync(code);
        }
    }
}
