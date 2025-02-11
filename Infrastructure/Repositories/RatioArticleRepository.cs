using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class RatioArticleRepository(LaboratoireCtsContext context) : IRatioArticleRepository
    {
        private readonly LaboratoireCtsContext _context = context;

        public async Task<RatioArticle> AddAsync(RatioArticle ratioArticle)
        {
            _context.RatioArticles.Add(ratioArticle); 
            await _context.SaveChangesAsync(); 
            return ratioArticle;
        }

        public async Task<RatioArticle> deleteRatioAsync(RatioArticle ratioArticle)
        {
            _context.RatioArticles.Remove(ratioArticle);
            await _context.SaveChangesAsync();
            return ratioArticle;
        }

        public async Task<IEnumerable<RatioArticle>> GetAllByCodeArticleAsync(string codeArticle)
        {
            return await _context.RatioArticles.Where(r => r.CodeArticle == codeArticle).ToListAsync();
        }

        public async Task<RatioArticle> GetByIdAsync(int id)
        {
            return await _context.RatioArticles.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(RatioArticle ratioArticle)
        {
            _context.RatioArticles.Update(ratioArticle);
            await _context.SaveChangesAsync();
        }
    }
}
