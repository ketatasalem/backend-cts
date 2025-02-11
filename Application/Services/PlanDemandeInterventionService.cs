using AutoMapper;
using FluentValidation;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Application.Validators;
using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Shared.IServices;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Application.Services
{
    public class PlanDemandeInterventionService(IPlanDemandeInterventionRepository planDemandeInterventionRepository, IMapper mapper, IValidator<PlanDemandeInterventionCreateDto> validator, ICommonService commonService, IRepository<PlansDemandesIntervention> repository) : IPlanDemandeInterventionService
    {
        private readonly IPlanDemandeInterventionRepository _planDemandeInterventionRepository = planDemandeInterventionRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<PlanDemandeInterventionCreateDto> _validator = validator;
        private readonly ICommonService _commonService = commonService;
        private readonly IRepository<PlansDemandesIntervention> _repository = repository;

        public async Task<ApiResponse<PlanDemandeInterventionCreateDto>> CreatePlanDemandeInterventionAsync(PlanDemandeInterventionCreateDto planDemandeInterventionCreateDto)
        {
            var validationResponse = _commonService.ValidateEntity(planDemandeInterventionCreateDto, _validator);
            if (!validationResponse.Success)
            {
                Console.WriteLine($"Erreur de validation : {string.Join(", ", validationResponse.Errors)}");
                return validationResponse;
            }

            return await _commonService.ExecuteSafely(async () =>
            {
                var verifPlan = await _planDemandeInterventionRepository.GetByCodeAsync(planDemandeInterventionCreateDto.Code);
                if (verifPlan == null)
                {
                    var plan = _mapper.Map<PlansDemandesIntervention>(planDemandeInterventionCreateDto);
                    await _commonService.UpdateTimeAndUserForUpdateFields(plan, "AdminUser", true);
                    await _planDemandeInterventionRepository.AddAsync(plan);

                    return new ApiResponse<PlanDemandeInterventionCreateDto>
                    {
                        Success = true,
                        Data = planDemandeInterventionCreateDto,
                        Message = "Plan créé avec succéss",
                        StatusCode = 201
                    };
                }
                return new ApiResponse<PlanDemandeInterventionCreateDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Conflits existent dans la création",
                    StatusCode = 405
                };
            });
        }

        public async Task<ApiResponse<IEnumerable<PlanDemandeInterventionResponseDto>>> GetAllPlansDemandeInterventionAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<IEnumerable<PlanDemandeInterventionResponseDto>>> GetAllPlansDemandeInterventionByTypeAsync(string type)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<PagedResult<PlanDemandeInterventionResponseDto>>> GetPagedPlansDemandeInterventionAsync(FilterQuery query)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<PlanDemandeInterventionResponseDto>> GetPlanDemandeInterventionByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<PlanDemandeInterventionResponseDto>> GetPlanDemandeInterventionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<PlanDemandeInterventionCreateDto>> UpdatePlanDemandeInterventionAsyncAsync(int idPlan, PlanDemandeInterventionCreateDto planDemandeInterventionCreateDto)
        {
            throw new NotImplementedException();
        }
    }
}
