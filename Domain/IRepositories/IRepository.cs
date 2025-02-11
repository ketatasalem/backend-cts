using Labo_Cts_backend.Shared.Models;
using System.Linq.Expressions;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<PagedResult<T>> GetPagedAsync(FilterQuery query);
    }

}
