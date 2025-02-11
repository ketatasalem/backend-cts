using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Application.IServices
{
    public interface IGammesChimiquesAlternativeService
    {
        Task<ApiResponse<IEnumerable<AlternativeResponseDto>>> GetAllAlternativesOfReferenceBainAsync(int codeBain);
        Task<ApiResponse<GammesChimiquesAlternative>> GetGammeChimiqueAlternativeByIdAsync(int id);
        Task<ApiResponse<IEnumerable<AlternativeLastVersionResponseDto>>> GetAllAlternativesOfReferenceBainWithLastVersionAsync(int codeBain);
        Task<ApiResponse<PagedResult<BainReferenceAlternativeViewResponseDto>>> GetPagedAllAlternativesAsync(FilterQuery query);
        Task<ApiResponse<AlternativeResponseDto>> GetAlternativeByCodeAsync(int codeAlternative);
        Task<ApiResponse<AlternativeCreateDto>> CreateAlternativeAsync(AlternativeCreateDto alternativeCreateDto);
        Task<ApiResponse<AlternativeResponseDto>> UpdateAlternativeAsync(int idAlt, GammeChimiqueAlternativeUpdateDto alternativeUpdateDto);

    }
}
