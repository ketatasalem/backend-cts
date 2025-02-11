using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Labo_Cts_backend.Shared.IServices;
using Labo_Cts_backend.Shared.Models;
using Labo_Cts_backend.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Application.Services
{
    public class ArticleService(IArticleRepository articleRepository, LaboratoireCtsContext dbContext, IMapper mapper, IValidator<ArticleUpdateDto> validator, IRepository<Article> repository, ICommonService commonService, IRatioArticleRepository ratioArticleRepository) : IArticleService
    {
        private readonly IArticleRepository _articleRepository = articleRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<ArticleUpdateDto> _validator = validator;
        private readonly ICommonService _commonService = commonService;
        private readonly IRepository<Article> _repository = repository;
        private readonly IRatioArticleRepository _ratioArticleRepository = ratioArticleRepository;
        private readonly LaboratoireCtsContext _dbContext = dbContext;

        public async Task<ApiResponse<IEnumerable<ArticleResponseDto>>> GetAllArticlesAsync()
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                var articles = await _articleRepository.GetAllAsync();

                if (!articles.Any())
                {
                    return new ApiResponse<IEnumerable<ArticleResponseDto>>
                    {
                        Success = false,
                        Message = "Non articles trouvés",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var articleDtos = _mapper.Map<IEnumerable<ArticleResponseDto>>(articles);

                return new ApiResponse<IEnumerable<ArticleResponseDto>>
                {
                    Success = true,
                    Data = articleDtos,
                    Message = "Les articles ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }

        public async Task<ApiResponse<ArticleResponseDto>> GetArticleByCodeAsync(string code)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                if (!_commonService.IsValidCode(code, "Identifiant", out ApiResponse<ArticleResponseDto> errorResponse))
                    return errorResponse;


                var article = await _articleRepository.GetByCodeAsync(code);

                if (article == null)
                {
                    return new ApiResponse<ArticleResponseDto>
                    {
                        Success = false,
                        Message = "Article non trouvé",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var articleDto = _mapper.Map<ArticleResponseDto>(article);

                return new ApiResponse<ArticleResponseDto>
                {
                    Success = true,
                    Data = articleDto,
                    Message = "L'article a été retourné avec succès",
                    StatusCode = StatusCodes.Status200OK
                };

            });
        }

        public async Task<ApiResponse<ArticleUpdateDto>> UpdateArticleAsync(string code, ArticleUpdateDto articleDto)
        {
            if (!_commonService.IsValidCode(code, "Identifiant", out ApiResponse<ArticleUpdateDto> errorResponse))
                return errorResponse;

            var validationResponse = _commonService.ValidateEntity(articleDto, _validator);
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
                    var article = await _articleRepository.GetByCodeAsync(code);
                    if (article == null)
                    {
                        return new ApiResponse<ArticleUpdateDto>
                        {
                            Success = false,
                            Message = "Article non trouvé",
                            StatusCode = StatusCodes.Status404NotFound
                        };
                    }

                    // Mapping depuis le DTO vers l'entité Article
                    _mapper.Map(articleDto, article);
                    article.RatioArticles = null;
                    await _commonService.UpdateTimeAndUserForUpdateFields(article, "AdminUser", false);

                    // Mise à jour dans le repository
                    var articleAdded = await _articleRepository.UpdateAsync(article);

                    // Vous pouvez mettre à jour les ratios par la suite, ici la partie est commentée.
                    if ((bool)article.EstRatio)
                    {
                        await CreateOrUpdateRatioArticle(articleDto.RatioArticles,article.Code);
                    }

                    await transaction.CommitAsync();
                    return new ApiResponse<ArticleUpdateDto>
                    {
                        Success = true,
                        Data = _mapper.Map<ArticleUpdateDto>(article),
                        Message = "Article mis à jour avec succès",
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

        public async Task<ApiResponse<PagedResult<ArticleResponseDto>>> GetPagedArticlesAsync(FilterQuery query)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                var pagedResult = await _repository.GetPagedAsync(query);

                if (!pagedResult.Items.Any())
                {
                    return new ApiResponse<PagedResult<ArticleResponseDto>>
                    {
                        Success = false,
                        Message = "Non articles trouvés",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var dtoItems = _mapper.Map<IEnumerable<ArticleResponseDto>>(pagedResult.Items);
                PagedResult<ArticleResponseDto> result = new PagedResult<ArticleResponseDto>
                {
                    Items = dtoItems,
                    TotalCount = pagedResult.TotalCount,
                    PageSize = pagedResult.PageSize,
                    CurrentPage = pagedResult.CurrentPage
                };

                return new ApiResponse<PagedResult<ArticleResponseDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Les articles ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };
            });
        }
        public async Task<ApiResponse<IEnumerable<RatioArticleResponseDto>>> GetRatioArticlesByCodeArticleAsync(string code)
        {
            return await _commonService.ExecuteSafely(async () =>
            {
                if (!_commonService.IsValidCode(code, "Identifiant article", out ApiResponse<IEnumerable<RatioArticleResponseDto>> errorResponse))
                    return errorResponse;


                var ratios = await _ratioArticleRepository.GetAllByCodeArticleAsync(code);

                if (ratios == null)
                {
                    return new ApiResponse<IEnumerable<RatioArticleResponseDto>>
                    {
                        Success = false,
                        Message = "Ratio article non trouvé",
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                var ratiosDto = _mapper.Map<IEnumerable<RatioArticleResponseDto>>(ratios);

                return new ApiResponse<IEnumerable<RatioArticleResponseDto>>
                {
                    Success = true,
                    Data = ratiosDto,
                    Message = "Les ratios d'un article ont été retournés avec succès",
                    StatusCode = StatusCodes.Status200OK
                };

            });
        }
        private async Task<List<RatioArticleCreateDto>> CreateOrUpdateRatioArticle(List<RatioArticleCreateDto> ratioArticleCreateDtos,string codeArticle)
        {
            // Mapper les DTO en entités RatioArticle
            var listeRatio = _mapper.Map<IEnumerable<RatioArticle>>(ratioArticleCreateDtos);

            var listeRatioExistant = await _ratioArticleRepository.GetAllByCodeArticleAsync(codeArticle);
            if(listeRatioExistant != null)
            {
                foreach(var r in listeRatioExistant)
                {
                    await _ratioArticleRepository.deleteRatioAsync(r);
                }
            }

            foreach (var ratio in listeRatio)
            {
                ratio.Id = 0;
                ratio.CodeArticle = codeArticle;
                    // Il s'agit d'une création : utiliser l'objet 'ratio' (non null) obtenu par mapping.
                    await _commonService.UpdateTimeAndUserForUpdateFields(ratio, "AdminUser", true);
                    await _ratioArticleRepository.AddAsync(ratio);
              
            }
            return ratioArticleCreateDtos;
        }
    }
}
