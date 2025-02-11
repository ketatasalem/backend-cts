using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Application.IServices
{
    public interface IPlanDemandeInterventionService
    {
        Task<ApiResponse<PlanDemandeInterventionCreateDto>> CreatePlanDemandeInterventionAsync(PlanDemandeInterventionCreateDto planDemandeInterventionCreateDto);
        Task<ApiResponse<PlanDemandeInterventionCreateDto>> UpdatePlanDemandeInterventionAsyncAsync(int idPlan, PlanDemandeInterventionCreateDto planDemandeInterventionCreateDto);
        Task<ApiResponse<IEnumerable<PlanDemandeInterventionResponseDto>>> GetAllPlansDemandeInterventionAsync();
        Task<ApiResponse<IEnumerable<PlanDemandeInterventionResponseDto>>> GetAllPlansDemandeInterventionByTypeAsync(string type);
        Task<ApiResponse<PlanDemandeInterventionResponseDto>> GetPlanDemandeInterventionByIdAsync(int id);
        Task<ApiResponse<PlanDemandeInterventionResponseDto>> GetPlanDemandeInterventionByCodeAsync(string code);
        Task<ApiResponse<PagedResult<PlanDemandeInterventionResponseDto>>> GetPagedPlansDemandeInterventionAsync(FilterQuery query);
    }
}
