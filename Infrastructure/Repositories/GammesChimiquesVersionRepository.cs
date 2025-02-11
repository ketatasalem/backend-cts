using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class GammesChimiquesVersionRepository(LaboratoireCtsContext context/*IDbContextFactory<LaboratoireCtsContext> contextFactory*/) : IGammesChimiquesVersionRepository
    {
        private readonly LaboratoireCtsContext _context = context;
        //private readonly IDbContextFactory<LaboratoireCtsContext> _contextFactory = contextFactory;

        public async Task<GammesChimiquesVersion> AddAsync(GammesChimiquesVersion gammesChimiquesVersion)
        {
            /*using var context = _contextFactory.CreateDbContext();
            context.GammesChimiquesVersions.Add(gammesChimiquesVersion);
            await context.SaveChangesAsync();
            return gammesChimiquesVersion;*/
            _context.GammesChimiquesVersions.Add(gammesChimiquesVersion);
            await _context.SaveChangesAsync();
            return gammesChimiquesVersion;
        }

        public async Task<IEnumerable<GammesChimiquesVersion>> GetByIdAlternative(int idAlternative)
        {
            /*using var context = _contextFactory.CreateDbContext();
            return await context.GammesChimiquesVersions
                .Where(v => v.IdGammeAlternative == idAlternative).ToListAsync();*/
            return await _context.GammesChimiquesVersions
                .Where(v => v.IdGammeAlternative == idAlternative).ToListAsync();
        }

        public async Task<GammesChimiquesVersion> GetLatestVersionOfAlternative(int idAlternative)
        {
            //using var context = _contextFactory.CreateDbContext();
            return await context.GammesChimiquesVersions
                .Where(v => v.IdGammeAlternative == idAlternative)
                .Include(v => v.ParametresVersions)
                .Include(v => v.ArticlesVersions)
                .OrderByDescending(v => v.DateDebutValidite).FirstOrDefaultAsync();
        }

        public async Task<GammesChimiquesVersion> GetVersionById(int id)
        {
            return await context.GammesChimiquesVersions
                .Where(v => v.Id == id)
                .Include(v => v.ParametresVersions)
                .Include(v => v.ArticlesVersions)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(GammesChimiquesVersion gammesChimiquesVersion)
        {
           // using var context = _contextFactory.CreateDbContext();
            context.Attach(gammesChimiquesVersion); // Attachez l'entité au contexte
            context.Entry(gammesChimiquesVersion).State = EntityState.Modified; // Marquez-la comme modifiée
            await context.SaveChangesAsync();
        }
    }
}
