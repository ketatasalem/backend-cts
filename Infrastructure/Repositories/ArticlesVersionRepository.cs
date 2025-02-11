using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class ArticlesVersionRepository(LaboratoireCtsContext context/*IDbContextFactory<LaboratoireCtsContext> contextFactory*/) : IArticlesVersionRepository
    {
        private readonly LaboratoireCtsContext _context = context;
        //private readonly IDbContextFactory<LaboratoireCtsContext> _contextFactory = contextFactory;

        public async Task<ArticlesVersion> AddAsync(ArticlesVersion articlesVersion)
        {
            /*using var context = _contextFactory.CreateDbContext();
            context.ArticlesVersions.Add(articlesVersion); // Ajout direct
            await context.SaveChangesAsync(); // Sauvegarde dans la base
            return articlesVersion;*/
            _context.ArticlesVersions.Add(articlesVersion); // Ajout direct
            await _context.SaveChangesAsync(); // Sauvegarde dans la base
            return articlesVersion;
        }
    }

}
