using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IPosteChargeRepository
    {
        Task<IEnumerable<PostesDeCharge>> GetAllAsync();
        Task<PostesDeCharge> GetByCodeAsync(String code);
    }
}
