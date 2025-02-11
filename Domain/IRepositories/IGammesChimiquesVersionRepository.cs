using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IGammesChimiquesVersionRepository
    {
        Task<GammesChimiquesVersion> AddAsync(GammesChimiquesVersion gammesChimiquesVersion);
        Task<IEnumerable<GammesChimiquesVersion>> GetByIdAlternative(int idAlternative);
        Task UpdateAsync(GammesChimiquesVersion gammesChimiquesVersion);
        Task<GammesChimiquesVersion> GetLatestVersionOfAlternative(int idAlternative);
        Task<GammesChimiquesVersion> GetVersionById(int id);
    }
}
