using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.DTOs.Request
{
    public class GammeChimiqueVersionCreateDto
    {
        public int? Id { get; set; }

        public string Nom { get; set; } = null!;

        public DateTime DateDebutValidite { get; set; }
        public int IdGammeAlternative { get; set; }

        public virtual ICollection<ArticleVersionCreateDto> ArticlesVersions { get; set; } = new List<ArticleVersionCreateDto>();
        public virtual ICollection<ParametreVersionCreateDto> ParametresVersions { get; set; } = new List<ParametreVersionCreateDto>();
    }
}
