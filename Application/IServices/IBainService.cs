using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Application.IServices
{
    public interface IBainService
    {
        Task<ApiResponse<BainCreateDto>> CreateBainAsync(BainCreateDto bainCreateDto);
        Task<ApiResponse<BainUpdateDto>> UpdateBainAsync(int bainCode, BainUpdateDto bainUpdateDto);
        Task<ApiResponse<IEnumerable<BainResponseDto>>> GetAllBainsAsync();
        Task<ApiResponse<BainResponseDto>> GetBainByCodeAsync(int code);
        Task<ApiResponse<PagedResult<BainResponseDto>>> GetPagedBainsAsync(FilterQuery query);
        Task<ApiResponse<IEnumerable<BainResponseDto>>> GetReferenceBainsByCTSAsync(string cts);
    }
}
