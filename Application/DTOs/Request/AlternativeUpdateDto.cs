using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.DTOs.Request
{
    public class AlternativeUpdateDto
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public DateTime? DateDebutValidite { get; set; }
        public bool EstParDefaut { get; set; }
        public List<GammesChimiquesVersion> GammesChimiquesVersions { get; set; } = new();
        public List<ArticleVersionCreateDto>? Articles { get; set; } = new();
        public List<ParametreVersionCreateDto>? Parametres { get; set; } = new();
    }
}
