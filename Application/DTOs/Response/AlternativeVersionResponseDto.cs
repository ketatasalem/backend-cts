using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.DTOs.Response
{
    public class AlternativeVersionResponseDto
    {
        public int Id { get; set; }

        public string Nom { get; set; } = null!;

        public DateTime DateDebutValidite { get; set; }

        public DateTime? DateFinValidite { get; set; }

        public int IdGammeAlternative { get; set; }
        public virtual ICollection<ArticleVersionResponseDto> ArticlesVersions { get; set; } = new List<ArticleVersionResponseDto>();
        public virtual ICollection<ParametreVersionResponseDto> ParametresVersions { get; set; } = new List<ParametreVersionResponseDto>();

    }
}
