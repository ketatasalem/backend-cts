using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IRatioArticleRepository
    {
        Task<RatioArticle> AddAsync(RatioArticle ratioArticle);
        Task<IEnumerable<RatioArticle>> GetAllByCodeArticleAsync(string codeArticle);
        Task UpdateAsync(RatioArticle ratioArticle);
        Task<RatioArticle> GetByIdAsync(int id);
        Task<RatioArticle> deleteRatioAsync(RatioArticle ratioArticle);
    }
}
