using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Domain.IRepositories
{
    public interface IPlanDemandeInterventionRepository
    {
        Task<PlansDemandesIntervention> GetByIdAsync(int id);
        Task<PlansDemandesIntervention> GetByCodeAsync(string code);
        Task<IEnumerable<PlansDemandesIntervention>> GetAllAsync();
        Task<IEnumerable<PlansDemandesIntervention>> GetAllByTypeInterventionAsync(string type);
        Task<PlansDemandesIntervention> AddAsync(PlansDemandesIntervention plansDemandesIntervention);
        Task UpdateAsync(PlansDemandesIntervention plansDemandesIntervention);
    }
}
