using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class BainRepository(LaboratoireCtsContext context/*IDbContextFactory<LaboratoireCtsContext> contextFactory*/) : IBainRepository
    {
        private readonly LaboratoireCtsContext _context = context;
        //private readonly IDbContextFactory<LaboratoireCtsContext> _contextFactory = contextFactory;

        public async Task<Bain> AddAsync(Bain bain)
        {
            /*using var context = _contextFactory.CreateDbContext();
            context.Bains.Add(bain); // Ajout direct
            await context.SaveChangesAsync(); // Sauvegarde dans la base
            return bain;*/
            _context.Bains.Add(bain); // Ajout direct
            await _context.SaveChangesAsync(); // Sauvegarde dans la base
            return bain;
        }

        /*public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _contextFactory.CreateDbContext().Database.BeginTransactionAsync();
        }*/


        public async Task<IEnumerable<Bain>> GetAllAsync()
        {
            /*using var context = _contextFactory.CreateDbContext();
            return await context.Bains
                .Include(b => b.GammesChimiquesAlternatives).ToListAsync();*/
           return await _context.Bains
                .Include(b => b.GammesChimiquesAlternatives).ToListAsync(); // Récupération directe
        }

        public async Task<IEnumerable<Bain>> GetAllByBainReferenceAsync(int code)
        {
            return await _context.Bains.Where(b => b.BainReferenceCode == code).ToListAsync();
        }

        public async Task<Bain> GetByCodeAsync(int code)
        {
            /*using var context = _contextFactory.CreateDbContext();
            return await context.Bains.Include(b => b.GammesChimiquesAlternatives)
                .FirstOrDefaultAsync(b => b.Code == code);*/
            return await context.Bains.Include(b => b.GammesChimiquesAlternatives)
                .FirstOrDefaultAsync(b => b.Code == code);
        }

        public async Task<IEnumerable<Bain>> GetReferenceBainsAsync()
        {
            return await _context.Bains
               .Where(b => b.EstReference == true)
               .Include(b => b.GammesChimiquesAlternatives)
                   .ThenInclude(a => a.GammesChimiquesVersions)
                   .ThenInclude(a => a.ArticlesVersions)
               .Include(b => b.GammesChimiquesAlternatives)
                   .ThenInclude(a => a.GammesChimiquesVersions)
                   .ThenInclude(a => a.ParametresVersions)
               .ToListAsync();
        }

        public async Task<IEnumerable<Bain>> GetReferenceBainsByCTSAsync(string cts)
        {
            return await _context.Bains
               .Where(b => b.EstReference == true && b.CodePosteCharge == cts).ToListAsync();
        }

        public async Task UpdateAsync(Bain bain)
        {
            /*using var context = _contextFactory.CreateDbContext();
            context.Update(bain);
            await context.SaveChangesAsync();*/
            _context.Bains.Update(bain);
            await _context.SaveChangesAsync();
        }
    }

}
