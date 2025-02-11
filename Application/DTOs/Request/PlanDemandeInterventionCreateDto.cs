namespace Labo_Cts_backend.Application.DTOs.Request
{
    public class PlanDemandeInterventionCreateDto
    {
        public string Code { get; set; } = null!;

        public string Intitule { get; set; } = null!;

        public string? Frequence { get; set; }

        public int? Compteur { get; set; }

        public string? CodePosteCharge { get; set; }

        public string? CodeBain { get; set; }
    }
}
