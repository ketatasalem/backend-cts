using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.DTOs.Response;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.Mappings
{
    public class PlanDemandeInterventionProfile: Profile
    {
        public PlanDemandeInterventionProfile()
        {
            CreateMap<PlansDemandesIntervention, PlanDemandeInterventionCreateDto>().ReverseMap();
            CreateMap<PlansDemandesIntervention, PlanDemandeInterventionResponseDto>().ReverseMap();
            CreateMap<PlanDemandeInterventionCreateDto, PlanDemandeInterventionResponseDto>().ReverseMap();
        }
    }
}
