using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.Vues;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IBainReferenceAlternativeViewRepository
    {
        Task<IEnumerable<BainReferenceAlternativeView>> GetAllAsync();
    }
}
