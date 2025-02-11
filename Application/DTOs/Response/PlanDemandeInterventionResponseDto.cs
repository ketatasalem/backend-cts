namespace Labo_Cts_backend.Application.DTOs.Response
{
    public class PlanDemandeInterventionResponseDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Intitule { get; set; } = null!;

        public string? Frequence { get; set; }

        public int? Compteur { get; set; }

        public string? CodePosteCharge { get; set; }

        public string? CodeBain { get; set; }
    }
}
