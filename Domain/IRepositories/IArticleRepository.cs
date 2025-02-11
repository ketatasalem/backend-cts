using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetByCodeAsync(string code);
        Task<Article> UpdateAsync(Article article);
    }
}
