using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Repositories;
using Labo_Cts_backend.Shared.IServices;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Application.Services
{
    public class ParametreChimiqueService(IParametreChimiqueRepository parametreChimiqueRepository, ICommonService commonService, IMapper mapper) : IParametreChimiqueService
    {

        private readonly IParametreChimiqueRepository _parametreChimiqueRepository = parametreChimiqueRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ICommonService _commonService = commonService;

        public async Task<ApiResponse<IEnumerable<ParametreChimiqueResponseDto>>> GetAllParametersChimiqueAsync()
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                var parametres = await _parametreChimiqueRepository.GetAllAsync();

                if (!parametres.Any())
                {
                    return new ApiResponse<IEnumerable<ParametreChimiqueResponseDto>>
                    {
                        Success = false,
                        Message = "Non parametres trouvés",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var parametresDtos = _mapper.Map<IEnumerable<ParametreChimiqueResponseDto>>(parametres);

                return new ApiResponse<IEnumerable<ParametreChimiqueResponseDto>>
                {
                    Success = true,
                    Data = parametresDtos,
                    Message = "Les articles ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }

        public async Task<ApiResponse<ParametreChimiqueResponseDto>> GetParameterChimiqueByCodeAsync(string code)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                if (!_commonService.IsValidCode(code, "Identifiant", out ApiResponse<ParametreChimiqueResponseDto> errorResponse))
                    return errorResponse;

                var parametre = await _parametreChimiqueRepository.GetByCodeAsync(code);

                if (parametre == null)
                {
                    return new ApiResponse<ParametreChimiqueResponseDto>
                    {
                        Success = false,
                        Message = "Parametre non trouvé",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var parametreDto = _mapper.Map<ParametreChimiqueResponseDto>(parametre);

                return new ApiResponse<ParametreChimiqueResponseDto>
                {
                    Success = true,
                    Data = parametreDto,
                    Message = "Le paramètre a été retourné avec succès",
                    StatusCode = StatusCodes.Status200OK
                };

            });
        }
    }
}
