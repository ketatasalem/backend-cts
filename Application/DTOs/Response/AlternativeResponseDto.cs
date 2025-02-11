using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.DTOs.Response
{
    public class AlternativeResponseDto
    {
        public int Id { get; set; }

        public string Nom { get; set; } = null!;

        public DateTime DateDebutValidite { get; set; }

        public DateTime? DateFinValidité { get; set; }

        public bool EstParDefaut { get; set; }

        public int CodeBain { get; set; }
        public string? NomBain { get; set; }

        public string? EmplacementBain { get; set; }

        public virtual ICollection<AlternativeVersionResponseDto> GammesChimiquesVersions { get; set; } = new List<AlternativeVersionResponseDto>();
    }
}
