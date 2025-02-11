using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Domain.Vues;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class BainReferenceAlternativeViewRepository(LaboratoireCtsContext context) : IBainReferenceAlternativeViewRepository
    {
        private readonly LaboratoireCtsContext _context = context;

        public async Task<IEnumerable<BainReferenceAlternativeView>> GetAllAsync()
        {
            return await _context.BainsReferenceAlternatives.ToListAsync();

        }
    }
}
