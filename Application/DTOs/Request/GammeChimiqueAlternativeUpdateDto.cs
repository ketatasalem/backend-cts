using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.DTOs.Request
{
    public class GammeChimiqueAlternativeUpdateDto
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public DateTime? DateDebutValidite { get; set; }
        public bool EstParDefaut { get; set; }
        public List<GammeChimiqueVersionCreateDto> GammesChimiquesVersions { get; set; } = new();
    }
}
