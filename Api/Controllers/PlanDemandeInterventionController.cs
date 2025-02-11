using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Application.Services;
using Labo_Cts_backend.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo_Cts_backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanDemandeInterventionController(IPlanDemandeInterventionService planDemandeInterventionService) : ControllerBase
    {
        private readonly IPlanDemandeInterventionService _planDemandeInterventionService = planDemandeInterventionService;

        /// <summary>
        /// Crée un nouveau plan.
        /// </summary>
        /// <param name="planCreateDto">Les données pour créer un plan.</param>
        /// <returns>Le plan créé.</returns>
        [HttpPost]
        public async Task<ApiResponse<PlanDemandeInterventionCreateDto>> CreatePlanDemandeIntervention([FromBody] PlanDemandeInterventionCreateDto planCreateDto)
        {
            return await _planDemandeInterventionService.CreatePlanDemandeInterventionAsync(planCreateDto);
        }
    }
}
