using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Application.IServices
{
    public interface IPosteChargeService
    {
        Task<ApiResponse<IEnumerable<PosteChargeResponseDto>>> GetAllPosteChargeAsync();
        Task<ApiResponse<PagedResult<PosteChargeResponseDto>>> GetPagedPosteChargeAsync(FilterQuery query);
        Task<ApiResponse<PosteChargeResponseDto>> GetPosteChargeByCodeAsync(String code);
    }
}
