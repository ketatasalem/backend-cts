using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class ParametresVersionRepository(LaboratoireCtsContext context/*IDbContextFactory<LaboratoireCtsContext> contextFactory*/) : IParametresVersionRepository
    {
        private readonly LaboratoireCtsContext _context = context;
        //private readonly IDbContextFactory<LaboratoireCtsContext> _contextFactory = contextFactory;

        public async Task<ParametresVersion> AddAsync(ParametresVersion parametresVersion)
        {
            /*using var context = _contextFactory.CreateDbContext();
            context.ParametresVersions.Add(parametresVersion);
            await context.SaveChangesAsync();
            return parametresVersion;*/
            _context.ParametresVersions.Add(parametresVersion);
            await _context.SaveChangesAsync();
            return parametresVersion;
        }
    }
}
