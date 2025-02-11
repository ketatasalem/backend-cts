using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IArticlesVersionRepository
    {
        Task<ArticlesVersion> AddAsync(ArticlesVersion articlesVersion);
    }
}
