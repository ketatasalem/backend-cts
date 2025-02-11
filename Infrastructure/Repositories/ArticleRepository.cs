using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class ArticleRepository(LaboratoireCtsContext context/*IDbContextFactory<LaboratoireCtsContext> contextFactory*/) : IArticleRepository
    {
        private readonly LaboratoireCtsContext _context = context;
        //private readonly IDbContextFactory<LaboratoireCtsContext> _contextFactory = contextFactory;

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            /*using var context = _contextFactory.CreateDbContext();
            return await context.Articles.Include(a => a.ArticleDependanceNavigation).ToListAsync();*/
            return await _context.Articles.Include(a => a.ArticleDependanceNavigation).ToListAsync();
        }

        public async Task<Article?> GetByCodeAsync(string code)
        {
            /*using var context = _contextFactory.CreateDbContext();
            return await context.Articles.Include(a => a.ArticleDependanceNavigation).FirstOrDefaultAsync(a => a.Code == code);*/
            return await _context.Articles.Include(a => a.ArticleDependanceNavigation).FirstOrDefaultAsync(a => a.Code == code);
        }

        public async Task<Article> UpdateAsync(Article article)
        {
            /*using var context = _contextFactory.CreateDbContext();
            context.Articles.Update(article);
            await context.SaveChangesAsync();*/
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
            return article;
        }
    }
}
