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
    public class PosteChargeService(IPosteChargeRepository posteChargeRepository, IMapper mapper, IRepository<PostesDeCharge> repository, ICommonService commonService) : IPosteChargeService
    {
        private readonly IPosteChargeRepository _posteChargeRepository = posteChargeRepository;
        private readonly ICommonService _commonService = commonService;
        private readonly IMapper _mapper = mapper;
        private readonly IRepository<PostesDeCharge> _repository = repository;
        public async Task<ApiResponse<IEnumerable<PosteChargeResponseDto>>> GetAllPosteChargeAsync()
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                var postesDeCharges = await _posteChargeRepository.GetAllAsync();

                if (!postesDeCharges.Any())
                {
                    return new ApiResponse<IEnumerable<PosteChargeResponseDto>>
                    {
                        Success = false,
                        Message = "Non Postes de charge trouvés",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var postesDeChargesDtos = _mapper.Map<IEnumerable<PosteChargeResponseDto>>(postesDeCharges);

                return new ApiResponse<IEnumerable<PosteChargeResponseDto>>
                {
                    Success = true,
                    Data = postesDeChargesDtos,
                    Message = "Les postes de charge ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }

        public async Task<ApiResponse<PagedResult<PosteChargeResponseDto>>> GetPagedPosteChargeAsync(FilterQuery query)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                var pagedResult = await _repository.GetPagedAsync(query);

                if (!pagedResult.Items.Any())
                {
                    return new ApiResponse<PagedResult<PosteChargeResponseDto>>
                    {
                        Success = false,
                        Message = "Non Postes de Charge trouvés",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var dtoItems = _mapper.Map<IEnumerable<PosteChargeResponseDto>>(pagedResult.Items);
                PagedResult<PosteChargeResponseDto> result = new PagedResult<PosteChargeResponseDto>
                {
                    Items = dtoItems,
                    TotalCount = pagedResult.TotalCount,
                    PageSize = pagedResult.PageSize,
                    CurrentPage = pagedResult.CurrentPage
                };

                return new ApiResponse<PagedResult<PosteChargeResponseDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Les postes de charge ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }

        public async Task<ApiResponse<PosteChargeResponseDto>> GetPosteChargeByCodeAsync(string code)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                if (!_commonService.IsValidCode(code, "Identifiant", out ApiResponse<PosteChargeResponseDto> errorResponse))
                    return errorResponse;

                var posteChaarge = await _posteChargeRepository.GetByCodeAsync(code);

                if (posteChaarge == null)
                {
                    return new ApiResponse<PosteChargeResponseDto>
                    {
                        Success = false,
                        Message = "Poste Charge non trouvée",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var posteChaargeDto = _mapper.Map<PosteChargeResponseDto>(posteChaarge);

                return new ApiResponse<PosteChargeResponseDto>
                {
                    Success = true,
                    Data = posteChaargeDto,
                    Message = "La poste de charge a été retournée avec succès",
                    StatusCode = StatusCodes.Status200OK
                };

            });
        }
    }
}
