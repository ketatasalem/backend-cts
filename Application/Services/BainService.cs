using System;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Application.Validators;
using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Labo_Cts_backend.Shared.IServices;
using Labo_Cts_backend.Shared.Models;

namespace Labo_Cts_backend.Application.Services
{
    public class BainService(IBainRepository bainRepository, LaboratoireCtsContext dbContext, IGammeManagerService gammeManagerService, IPosteChargeService posteChargeService,IMapper mapper, IValidator<BainCreateDto> validator, IValidator<BainUpdateDto> validatorBainUpdate, ICommonService commonService, IRepository<Bain> repository) : IBainService
    {
        private readonly IBainRepository _bainRepository = bainRepository;
        private readonly IGammeManagerService _gammeManagerService = gammeManagerService;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<BainCreateDto> _validator = validator;
        private readonly IValidator<BainUpdateDto> _validatorUpdateBain = validatorBainUpdate;
        private readonly ICommonService _commonService = commonService;
        private readonly LaboratoireCtsContext _dbContext = dbContext;
        private readonly IRepository<Bain> _repository = repository;
        private readonly IPosteChargeService _posteChargeService = posteChargeService;

        public async Task<ApiResponse<BainCreateDto>> CreateBainAsync(BainCreateDto bainCreateDto)
        {
            var validationResponse = _commonService.ValidateEntity(bainCreateDto, _validator);
            if (!validationResponse.Success)
            {
                Console.WriteLine($"Erreur de validation : {string.Join(", ", validationResponse.Errors)}");
                return validationResponse;
            }

            return await _commonService.ExecuteSafely(async () =>
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    var bain = await CreateBainAsyncInternal(bainCreateDto);

                    if (bainCreateDto.EstReference)
                    {
                        foreach (var alternative in bainCreateDto.Alternatives)
                        {
                            var createdAlternative = await _gammeManagerService.CreateAlternativeAsync(alternative, bain.Code);

                            await _gammeManagerService.CreateVersionWithUpdates(
                                   _mapper.Map<AlternativeUpdateDto>(alternative), createdAlternative.Id);
                        }
                    }
                    await transaction.CommitAsync();
                    return new ApiResponse<BainCreateDto>
                    {
                        Success = true,
                        Data = bainCreateDto,
                        Message = "Bain créé avec succès",
                        StatusCode = StatusCodes.Status201Created
                    };
                }
                catch
                {
                    // Rollback en cas d'échec
                    await transaction.RollbackAsync();
                    throw;
                }
                
            });
        }

        public async Task<ApiResponse<BainUpdateDto>> UpdateBainAsync(int bainCode, BainUpdateDto bainUpdateDto)
        {
            var validationResponse = _commonService.ValidateEntity(bainUpdateDto, _validatorUpdateBain);
            if (!validationResponse.Success)
            {
                Console.WriteLine($"Erreur de validation : {string.Join(", ", validationResponse.Errors)}");
                return validationResponse;
            }

            return await _commonService.ExecuteSafely(async () =>
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    // Vérification de l'existence du bain
                    var existingBain = await _bainRepository.GetByCodeAsync(bainCode);

                    if (existingBain == null)
                    {
                        return new ApiResponse<BainUpdateDto>
                        {
                            Success = false,
                            Message = "Bain non trouvé",
                            StatusCode = StatusCodes.Status404NotFound
                        };
                    }

                    // Vérifiez et mettez à jour les données du bain
                    bool hasBainChanges = await HasBainChanges(bainUpdateDto, existingBain).ConfigureAwait(false);

                    if (hasBainChanges)
                    {
                        var bains = await _bainRepository.GetAllByBainReferenceAsync(existingBain.Code).ConfigureAwait(false);
                        if (bainUpdateDto.EstReference == false && bains.Count() > 0)
                        {
                            return new ApiResponse<BainUpdateDto>
                            {
                                Success = false,
                                Message = "Ce bain est une référence pour d'autre bains",
                                StatusCode = StatusCodes.Status400BadRequest
                            };
                        }

                        await UpdateBainEntityInternal(bainUpdateDto, existingBain).ConfigureAwait(false);
                    }

                        // Charger et traiter les alternatives
                  if(bainUpdateDto.Alternatives != null)
                    {
                        var alternatives = bainUpdateDto.Alternatives.ToList();
                        foreach (var alternativeUpdateDto in alternatives)
                        {
                            var existingAlternative = existingBain.GammesChimiquesAlternatives
                                .FirstOrDefault(a => a.Id == alternativeUpdateDto.Id);

                            if (existingAlternative == null)
                            {
                                var alternativeToCreate = _mapper.Map<AlternativeCreateDto>(alternativeUpdateDto);
                                var createdAlternative = await _gammeManagerService.CreateAlternativeAsync(alternativeToCreate, existingBain.Code);
                                Console.WriteLine($"Erreur de validation :");
                                await _gammeManagerService.CreateVersionWithUpdates(
                                   alternativeUpdateDto, createdAlternative.Id);
                                continue;
                            }

                            bool hasAlternativeChanges = await _gammeManagerService.HasAlternativeChanges(
                                alternativeUpdateDto, existingAlternative);

                            if (hasAlternativeChanges)
                            {
                                await _gammeManagerService.ChangeAlternativeDefaultBainValue(
                                    alternativeUpdateDto, existingAlternative).ConfigureAwait(false);
                            }

                            bool hasChanges = await _gammeManagerService.HasChanges(
                                alternativeUpdateDto).ConfigureAwait(false);

                            if (hasChanges)
                            {
                                await _gammeManagerService.MarkVersionAsEnded(existingAlternative.Id);
                                await _gammeManagerService.CreateVersionWithUpdates(
                                    alternativeUpdateDto, existingAlternative.Id);
                            }
                        }
                    }

                    await transaction.CommitAsync();

                    return new ApiResponse<BainUpdateDto>
                    {
                        Success = true,
                        Data = _mapper.Map<BainUpdateDto>(existingBain),
                        Message = "Bain mis à jour avec succès",
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }


        public async Task<ApiResponse<IEnumerable<BainResponseDto>>> GetReferenceBainsByCTSAsync(string cts)
        {
            var responseCts = await _posteChargeService.GetPosteChargeByCodeAsync(cts);
            if (responseCts == null)
            {
                return new ApiResponse<IEnumerable<BainResponseDto>>
                {
                    Success = false,
                    Message = "CTS non trouvé",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var bains = await _bainRepository.GetReferenceBainsByCTSAsync(cts);

            if (!bains.Any())
            {
                return new ApiResponse<IEnumerable<BainResponseDto>>
                {
                    Success = false,
                    Message = "Non Bains trouvés",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var bainDtos = _mapper.Map<IEnumerable<BainResponseDto>>(bains);

            return new ApiResponse<IEnumerable<BainResponseDto>>
            {
                Success = true,
                Data = bainDtos,
                Message = "Les bains ont été retournés avec succès",
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<ApiResponse<IEnumerable<BainResponseDto>>> GetAllBainsAsync()
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                var bains = await _bainRepository.GetAllAsync();

                if (!bains.Any())
                {
                    return new ApiResponse<IEnumerable<BainResponseDto>>
                    {
                        Success = false,
                        Message = "Non Bains trouvés",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var bainDtos = _mapper.Map<IEnumerable<BainResponseDto>>(bains);

                return new ApiResponse<IEnumerable<BainResponseDto>>
                {
                    Success = true,
                    Data = bainDtos,
                    Message = "Les bains ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }

        public async Task<ApiResponse<BainResponseDto>> GetBainByCodeAsync(int code)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                if (!_commonService.IsValidCode(code, "Identifiant", out ApiResponse<BainResponseDto> errorResponse))
                    return errorResponse;

                var article = await _bainRepository.GetByCodeAsync(code);

                if (article == null)
                {
                    return new ApiResponse<BainResponseDto>
                    {
                        Success = false,
                        Message = "Bain non trouvé",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var articleDto = _mapper.Map<BainResponseDto>(article);

                return new ApiResponse<BainResponseDto>
                {
                    Success = true,
                    Data = articleDto,
                    Message = "Le bain a été retourné avec succès",
                    StatusCode = StatusCodes.Status200OK
                };

            });

        }

        public async Task<ApiResponse<PagedResult<BainResponseDto>>> GetPagedBainsAsync(FilterQuery query)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                var pagedResult = await _repository.GetPagedAsync(query);

                if (!pagedResult.Items.Any())
                {
                    return new ApiResponse<PagedResult<BainResponseDto>>
                    {
                        Success = false,
                        Message = "Non Bains trouvés",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var dtoItems = _mapper.Map<IEnumerable<BainResponseDto>>(pagedResult.Items);
                PagedResult<BainResponseDto> result = new PagedResult<BainResponseDto>
                {
                    Items = dtoItems,
                    TotalCount = pagedResult.TotalCount,
                    PageSize = pagedResult.PageSize,
                    CurrentPage = pagedResult.CurrentPage
                };

                return new ApiResponse<PagedResult<BainResponseDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Les bains ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }


        private async Task<Bain> CreateBainAsyncInternal(BainCreateDto bainCreateDto)
        {
            var bain = _mapper.Map<Bain>(bainCreateDto);
            _commonService.UpdateTimeAndUserForUpdateFields(bain, "AdminUser", true);
            //S'il sagit d'un bain non référence, j'affecte id de l'alternative et de la version du bain référence a ce bain
            if (bainCreateDto.BainReferenceCode >= 0 || bainCreateDto.BainReferenceCode != null)
            {
                var referenceBain = await _bainRepository.GetByCodeAsync((int)bainCreateDto.BainReferenceCode);
                bain.IdAlternativeGammeChimiqueActif = referenceBain.IdAlternativeGammeChimiqueActif;
                bain.IdVersionGammeChimiqueActif = referenceBain.IdVersionGammeChimiqueActif;
            }
            return await _bainRepository.AddAsync(bain);
        }

        private async Task<bool> HasBainChanges(BainUpdateDto bainUpdateDto, Bain existingBain)
        {
            var hasBainChanges = bainUpdateDto.Emplacement != existingBain.Emplacement ||
                bainUpdateDto.CodePosteCharge != existingBain.CodePosteCharge ||
                bainUpdateDto.EstReference != existingBain.EstReference ||
                bainUpdateDto.Actif != existingBain.Actif ||
                bainUpdateDto.DimensionHauteur != existingBain.DimensionHauteur ||
                bainUpdateDto.DimensionLargeur != existingBain.DimensionLargeur ||
                bainUpdateDto.BainReferenceCode != existingBain.BainReferenceCode ||
                bainUpdateDto.DimensionLongueur != existingBain.DimensionLongueur;
            return hasBainChanges;
        }

        private async Task UpdateBainEntityInternal(BainUpdateDto bainUpdateDto, Bain existingBain)
        {
            _mapper.Map(bainUpdateDto, existingBain);
            _commonService.UpdateTimeAndUserForUpdateFields(existingBain, "AdminUser", false);
            await _bainRepository.UpdateAsync(existingBain);
        }
    }
}
