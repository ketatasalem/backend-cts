using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IParametreChimiqueRepository
    {
        Task<IEnumerable<ParametresChimique>> GetAllAsync();
        Task<ParametresChimique?> GetByCodeAsync(string code);
    }
}
