using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Application.IServices
{
    public interface IParametreChimiqueService
    {
        Task<ApiResponse<IEnumerable<ParametreChimiqueResponseDto>>> GetAllParametersChimiqueAsync();
        Task<ApiResponse<ParametreChimiqueResponseDto>> GetParameterChimiqueByCodeAsync(string code);


    }
}
