using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class GammesChimiquesAlternativeRepository(LaboratoireCtsContext context/*IDbContextFactory<LaboratoireCtsContext> contextFactory*/) : IGammesChimiquesAlternativeRepository
    {
        private readonly LaboratoireCtsContext _context = context;
        //private readonly IDbContextFactory<LaboratoireCtsContext> _contextFactory = contextFactory;

        public async Task<GammesChimiquesAlternative> AddAsync(GammesChimiquesAlternative gammesChimiquesAlternative)
        {
            /*using var context = _contextFactory.CreateDbContext();
            context.GammesChimiquesAlternatives.Add(gammesChimiquesAlternative); 
            await context.SaveChangesAsync(); 
            return gammesChimiquesAlternative;*/
            _context.GammesChimiquesAlternatives.Add(gammesChimiquesAlternative); // Ajout direct
            await _context.SaveChangesAsync(); // Sauvegarde dans la base
            return gammesChimiquesAlternative;
        }

        public async Task<GammesChimiquesAlternative> GetByCodeAlternativeAsync(int codeAlternative)
        {
            return await _context.GammesChimiquesAlternatives
               .Where(a => a.Id == codeAlternative)
               .Include(a => a.GammesChimiquesVersions)
                   .ThenInclude(gv => gv.ArticlesVersions) // Inclure ArticlesVersions
               .Include(a => a.GammesChimiquesVersions)
                   .ThenInclude(gv => gv.ParametresVersions) // Inclure ParametresVersions
               .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<GammesChimiquesAlternative>> GetByCodeBainAsync(int codeBain)
        {
            /*using var context = _contextFactory.CreateDbContext();
            return await context.GammesChimiquesAlternatives
               .Where(a => a.CodeBain == codeBain)
               .Include(a => a.GammesChimiquesVersions).ToListAsync();*/
            return await _context.GammesChimiquesAlternatives
                .Where(a => a.CodeBain == codeBain)
                .Include(a => a.GammesChimiquesVersions)
                    .ThenInclude(gv => gv.ArticlesVersions) // Inclure ArticlesVersions
                .Include(a => a.GammesChimiquesVersions)
                    .ThenInclude(gv => gv.ParametresVersions) // Inclure ParametresVersions
                .ToListAsync();
        }

        public async Task<GammesChimiquesAlternative?> GetByIdAsync(int id)
        {
            /*using var context = _contextFactory.CreateDbContext();
            return await context.GammesChimiquesAlternatives
                .Include(a => a.GammesChimiquesVersions).FirstOrDefaultAsync(a => a.Id == id);*/
            return await _context.GammesChimiquesAlternatives
               .Include(a => a.GammesChimiquesVersions).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(GammesChimiquesAlternative gammesChimiquesAlternative)
        {
            //using var context = _contextFactory.CreateDbContext();
            _context.GammesChimiquesAlternatives.Update(gammesChimiquesAlternative);
            await _context.SaveChangesAsync();
        }
    }

}
