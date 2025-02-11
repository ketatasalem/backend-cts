using Labo_Cts_backend.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IBainRepository
    {
        Task<Bain> GetByCodeAsync(int code);
        Task<IEnumerable<Bain>> GetAllAsync();
        Task<Bain> AddAsync(Bain bain);
        Task UpdateAsync(Bain bain);
        Task<IEnumerable<Bain>> GetAllByBainReferenceAsync(int code);
        Task<IEnumerable<Bain>> GetReferenceBainsAsync();
        Task<IEnumerable<Bain>> GetReferenceBainsByCTSAsync(string cts);
    }
}
