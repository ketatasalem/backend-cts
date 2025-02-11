using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Infrastructure.Repositories
{
    public class PlanDemandeInterventionRepository(LaboratoireCtsContext context) : IPlanDemandeInterventionRepository
    {
        private readonly LaboratoireCtsContext _context = context;
        public async Task<PlansDemandesIntervention> AddAsync(PlansDemandesIntervention plansDemandesIntervention)
        {
            _context.PlansDemandesInterventions.Add(plansDemandesIntervention);
            await _context.SaveChangesAsync();
            return plansDemandesIntervention;
        }

        public async Task<IEnumerable<PlansDemandesIntervention>> GetAllAsync()
        {
            return await _context.PlansDemandesInterventions.ToListAsync();
        }

        public async Task<IEnumerable<PlansDemandesIntervention>> GetAllByTypeInterventionAsync(string type)
        {
            return await _context.PlansDemandesInterventions.Where(p => p.Code.StartsWith(type)).ToListAsync();
        }

        public async Task<PlansDemandesIntervention> GetByCodeAsync(string code)
        {
            return await context.PlansDemandesInterventions.FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<PlansDemandesIntervention> GetByIdAsync(int id)
        {
            return await context.PlansDemandesInterventions.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(PlansDemandesIntervention plansDemandesIntervention)
        {
            _context.PlansDemandesInterventions.Update(plansDemandesIntervention);
            await _context.SaveChangesAsync();
        }
    }
}
