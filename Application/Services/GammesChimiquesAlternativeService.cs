using AutoMapper;
using FluentValidation;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Domain.Vues;
using Labo_Cts_backend.Infrastructure.Context;
using Labo_Cts_backend.Infrastructure.Repositories;
using Labo_Cts_backend.Shared.IServices;
using Labo_Cts_backend.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Application.Services
{
    public class GammesChimiquesAlternativeService(IGammesChimiquesAlternativeRepository gammesChimiquesAlternativeRepository, ICommonService commonService,LaboratoireCtsContext dbContext, IGammeManagerService gammeManagerService, IBainService bainService,IValidator<AlternativeCreateDto> validator, IMapper mapper, IBainRepository bainRepository, IRepository<BainReferenceAlternativeView> repository) : IGammesChimiquesAlternativeService
    {
        private readonly IGammesChimiquesAlternativeRepository _gammesChimiquesAlternativeRepository = gammesChimiquesAlternativeRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IBainRepository _bainRepository = bainRepository;
        private readonly IRepository<BainReferenceAlternativeView> _repository = repository;
        private readonly IValidator<AlternativeCreateDto> _validator = validator;
        private readonly IBainService _bainService = bainService;
        private readonly IGammeManagerService _gammeManagerService = gammeManagerService;
        private readonly LaboratoireCtsContext _dbContext = dbContext;
        private readonly ICommonService _commonService = commonService;

        public async Task<ApiResponse<IEnumerable<AlternativeResponseDto>>> GetAllAlternativesOfReferenceBainAsync(int codeBain)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                if (!_commonService.IsValidCode(codeBain, "Identifiant", out ApiResponse<IEnumerable<AlternativeResponseDto>> errorResponse))
                    return errorResponse;

                var bain = await _bainRepository.GetByCodeAsync(codeBain);

                if (bain == null)
                {
                    return new ApiResponse<IEnumerable<AlternativeResponseDto>>
                    {
                        Success = false,
                        Message = "Bain non trouvé",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var alternativesOfBain = await _gammesChimiquesAlternativeRepository.GetByCodeBainAsync(codeBain);
                if (!alternativesOfBain.Any())
                {
                    return new ApiResponse<IEnumerable<AlternativeResponseDto>>
                    {
                        Success = false,
                        Message = "Non Alternatives trouvés",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var alternativesResponseOfBain = _mapper.Map<IEnumerable<AlternativeResponseDto>>(alternativesOfBain);
                return new ApiResponse<IEnumerable<AlternativeResponseDto>>
                {
                    Success = true,
                    Data = alternativesResponseOfBain,
                    Message = "Les Alternatives ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }

        public async Task<ApiResponse<IEnumerable<AlternativeLastVersionResponseDto>>> GetAllAlternativesOfReferenceBainWithLastVersionAsync(int codeBain)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                if (!_commonService.IsValidCode(codeBain, "Code bain", out ApiResponse<IEnumerable<AlternativeLastVersionResponseDto>> errorResponse))
                    return errorResponse;

                var bain = await _bainRepository.GetByCodeAsync(codeBain);

                if (bain == null)
                {
                    return new ApiResponse<IEnumerable<AlternativeLastVersionResponseDto>>
                    {
                        Success = false,
                        Message = "Bain non trouvé",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var alternativesOfBain = await _gammesChimiquesAlternativeRepository.GetByCodeBainAsync(codeBain);
                var filteredAlternatives = alternativesOfBain.Select(alt => new AlternativeLastVersionResponseDto
                {
                    Id = alt.Id,
                    Nom = alt.Nom,
                    DateDebutValidite = alt.DateDebutValidite,
                    EstParDefaut = alt.EstParDefaut,
                    Articles = alt.GammesChimiquesVersions
                    .Where(v => v.DateFinValidite == null) // Filtrer les versions sans DateFinValidite
                    .OrderByDescending(v => v.DateDebutValidite) // Trier par DateDebutValidite
                    .SelectMany(v => v.ArticlesVersions.Select(av => new ArticleVersionCreateDto
                    {
                        CodeArticle = av.CodeArticle,
                        ValeurMin = av.ValeurMin,
                        ValeurMax = av.ValeurMax,
                        Valeur = av.Valeur
                    }))
                    .ToList(), // Extraire tous les articles de la dernière version

                    Parametres = alt.GammesChimiquesVersions
                    .Where(v => v.DateFinValidite == null) // Filtrer les versions sans DateFinValidite
                    .OrderByDescending(v => v.DateDebutValidite) // Trier par DateDebutValidite
                    .SelectMany(v => v.ParametresVersions.Select(pv => new ParametreVersionCreateDto
                    {
                        IdParametreChimique = pv.IdParametreChimique,
                        ValeurMin = pv.ValeurMin,
                        ValeurMax = pv.ValeurMax,
                        Valeur = pv.Valeur
                    }))
                    .ToList() // Extraire tous les paramètres de la dernière version
                }).ToList();

                if (!alternativesOfBain.Any())
                {
                    return new ApiResponse<IEnumerable<AlternativeLastVersionResponseDto>>
                    {
                        Success = false,
                        Message = "Non Alternatives trouvés",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                
                return new ApiResponse<IEnumerable<AlternativeLastVersionResponseDto>>
                {
                    Success = true,
                    Data = filteredAlternatives,
                    Message = "Les Alternatives ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }

        public async Task<ApiResponse<GammesChimiquesAlternative>> GetGammeChimiqueAlternativeByIdAsync(int id)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                if (!_commonService.IsValidCode(id, "Code Alternative gamme chimique", out ApiResponse<GammesChimiquesAlternative> errorResponse))
                    return errorResponse;


                var alternative = await _gammesChimiquesAlternativeRepository.GetByIdAsync(id);

                if (alternative == null)
                {
                    return new ApiResponse<GammesChimiquesAlternative>
                    {
                        Success = false,
                        Message = "Alternative gamme chimique non trouvé",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }


                return new ApiResponse<GammesChimiquesAlternative>
                {
                    Success = true,
                    Data = alternative,
                    Message = "L'alternative gamme chimique a été retourné avec succès",
                    StatusCode = StatusCodes.Status200OK
                };

            });
        }

        public async Task<ApiResponse<PagedResult<BainReferenceAlternativeViewResponseDto>>> GetPagedAllAlternativesAsync(FilterQuery query)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                var pagedResult = await _repository.GetPagedAsync(query);

                if (!pagedResult.Items.Any())
                {
                    return new ApiResponse<PagedResult<BainReferenceAlternativeViewResponseDto>>
                    {
                        Success = false,
                        Message = "Non Alternatives trouvées",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var dtoItems = _mapper.Map<IEnumerable<BainReferenceAlternativeViewResponseDto>>(pagedResult.Items);
                PagedResult<BainReferenceAlternativeViewResponseDto> result = new PagedResult<BainReferenceAlternativeViewResponseDto>
                {
                    Items = dtoItems,
                    TotalCount = pagedResult.TotalCount,
                    PageSize = pagedResult.PageSize,
                    CurrentPage = pagedResult.CurrentPage
                };

                return new ApiResponse<PagedResult<BainReferenceAlternativeViewResponseDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Les bains ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }

        public async Task<ApiResponse<AlternativeResponseDto>> GetAlternativeByCodeAsync(int codeAlternative)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                if (!_commonService.IsValidCode(codeAlternative, "Code Alternative", out ApiResponse<AlternativeResponseDto> errorResponse))
                    return errorResponse;

                var alt = await _gammesChimiquesAlternativeRepository.GetByCodeAlternativeAsync(codeAlternative);

                if (alt == null)
                {
                    return new ApiResponse<AlternativeResponseDto>
                    {
                        Success = false,
                        Message = "Alternative non trouvé",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var alternativesResponseOfBain = _mapper.Map<AlternativeResponseDto>(alt);
                return new ApiResponse<AlternativeResponseDto>
                {
                    Success = true,
                    Data = alternativesResponseOfBain,
                    Message = "L'alternative a été retourné avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }

        public async Task<ApiResponse<AlternativeCreateDto>> CreateAlternativeAsync(AlternativeCreateDto alternativeCreateDto)
        {
            var validationResponse = _commonService.ValidateEntity(alternativeCreateDto, _validator);
            if (!validationResponse.Success)
            {
                Console.WriteLine($"Erreur de validation : {string.Join(", ", validationResponse.Errors)}");
                return validationResponse;
            }

            return await _commonService.ExecuteSafely(async () =>
            {
                if (!_commonService.IsValidCode((int)alternativeCreateDto.CodeBain, "Code Bain", out ApiResponse<AlternativeCreateDto> errorResponse))
                    return errorResponse;

                using var transaction = await _dbContext.Database.BeginTransactionAsync();

                try
                {
                    var responseBain = await _bainService.GetBainByCodeAsync((int)alternativeCreateDto.CodeBain);

                    if (responseBain == null)
                    {
                        return new ApiResponse<AlternativeCreateDto>
                        {
                            Success = false,
                            Message = "Le bain n'existe pas",
                            StatusCode = StatusCodes.Status404NotFound
                        };
                    }
               
                    var createdAlternative = await _gammeManagerService.CreateAlternativeAsync(alternativeCreateDto, (int)alternativeCreateDto.CodeBain);
                    await _gammeManagerService.CreateVersionWithUpdates(
                                  _mapper.Map<AlternativeUpdateDto>(alternativeCreateDto), createdAlternative.Id);
                    await transaction.CommitAsync();
                    return new ApiResponse<AlternativeCreateDto>
                    {
                        Success = true,
                        Data = alternativeCreateDto,
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

        public async Task<ApiResponse<AlternativeResponseDto>> UpdateAlternativeAsync(int idAlt, GammeChimiqueAlternativeUpdateDto alternativeUpdateDto)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    var alternativeResponse = await GetAlternativeByCodeAsync(idAlt);
                    if (alternativeResponse == null)
                    {
                        return new ApiResponse<AlternativeResponseDto>
                        {
                            Success = false,
                            Message = "Alternative n'existe pas",
                            StatusCode = StatusCodes.Status404NotFound
                        };
                    }

                    if (alternativeUpdateDto.GammesChimiquesVersions != null)
                    {
                        var versions = alternativeUpdateDto.GammesChimiquesVersions.ToList();
                        foreach (var version in versions)
                        {
                            bool hasChanged = await _gammeManagerService.HasChanges(version);

                            if (hasChanged)
                            {
                                await _gammeManagerService.MarkVersionAsEnded(version.IdGammeAlternative);
                                await _gammeManagerService.CreateVersionWithArticlesAndParameters(
                                    _mapper.Map<GammesChimiquesVersion>(version), version.IdGammeAlternative);
                            }
                        }
                    }

                    // Commit uniquement après toutes les opérations
                    await transaction.CommitAsync();

                    // Transformer après Commit pour éviter les erreurs
                    var responseDto = _mapper.Map<AlternativeResponseDto>(alternativeResponse.Data);

                    return new ApiResponse<AlternativeResponseDto>
                    {
                        Success = true,
                        Data = responseDto,
                        Message = "Alternative mis à jour avec succès",
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

    }
}
