using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IGammesChimiquesAlternativeRepository
    {
        Task<GammesChimiquesAlternative> AddAsync(GammesChimiquesAlternative gammesChimiquesAlternative);
        Task<IEnumerable<GammesChimiquesAlternative>> GetByCodeBainAsync(int codeBain);
        Task<GammesChimiquesAlternative?> GetByIdAsync(int id);
        Task UpdateAsync(GammesChimiquesAlternative gammesChimiquesAlternative);
        Task<GammesChimiquesAlternative> GetByCodeAlternativeAsync(int codeAlternative);
    }
}
