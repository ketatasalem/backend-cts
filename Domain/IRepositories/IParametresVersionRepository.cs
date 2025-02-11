using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IParametresVersionRepository
    {
        Task<ParametresVersion> AddAsync(ParametresVersion parametresVersion);
    }
}
