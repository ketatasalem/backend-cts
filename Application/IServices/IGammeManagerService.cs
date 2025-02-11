using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.IServices
{
    public interface IGammeManagerService
    {
        Task<GammesChimiquesAlternative> CreateAlternativeAsync(AlternativeCreateDto alt, int bainCode);
        Task AddParametersToVersionAsync(IEnumerable<ParametreVersionCreateDto> parametres, int versionId);
        Task AddArticlesToVersionAsync(IEnumerable<ArticleVersionCreateDto> articles, int versionId);
        Task UpdateBainActifAlternativeAndVersionAsync(int bainCode, int idAlternative, int idVersion);
        Task<bool> HasAlternativeChanges(AlternativeUpdateDto updatedAlternative, GammesChimiquesAlternative existingAlternative);
        Task ChangeAlternativeDefaultBainValue(AlternativeUpdateDto updatedAlternative, GammesChimiquesAlternative existingAlternative);
        Task MarkVersionAsEnded(int alternativeId);
        Task<GammesChimiquesVersion> CreateVersionWithUpdates(AlternativeUpdateDto updatedAlternative, int alternativeId);
        Task<GammesChimiquesVersion> CreateVersionWithArticlesAndParameters(GammesChimiquesVersion version, int alternativeId);
        Task<bool> HasChanges(object updatedDto);

    }
}
