using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class ParametreChimiqueRepository(LaboratoireCtsContext context) : IParametreChimiqueRepository
    {
        private readonly LaboratoireCtsContext _context = context;

        public async Task<IEnumerable<ParametresChimique>> GetAllAsync()
        {
            return await _context.ParametresChimiques.ToListAsync();
        }

        public async Task<ParametresChimique?> GetByCodeAsync(string code)
        {
            return await _context.ParametresChimiques.FirstOrDefaultAsync(p => p.Code == code);
        }
    }
}
