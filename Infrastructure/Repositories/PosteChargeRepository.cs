using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class PosteChargeRepository(LaboratoireCtsContext context) : IPosteChargeRepository
    {
        private readonly LaboratoireCtsContext _context = context;

        public async Task<IEnumerable<PostesDeCharge>> GetAllAsync()
        {
            return await _context.PostesDeCharges
                 .Include(p => p.CodeSiteNavigation)
                 .Include(p => p.CodeCentreChargeNavigation).ToListAsync(); // Récupération directe
        }

        public async Task<PostesDeCharge> GetByCodeAsync(string code)
        {
            return await context.PostesDeCharges.Include(p => p.CodeSiteNavigation)
                 .Include(p => p.CodeCentreChargeNavigation)
                .FirstOrDefaultAsync(b => b.Code == code);
        }
    }
}
