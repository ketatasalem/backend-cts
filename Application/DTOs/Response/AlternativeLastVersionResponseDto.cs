using Labo_Cts_backend.Application.DTOs.Request;

namespace Labo_Cts_backend.Application.DTOs.Response
{
    public class AlternativeLastVersionResponseDto
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public DateTime DateDebutValidite { get; set; }
        public bool EstParDefaut { get; set; }
        public string? NomBain { get; set; }
        public List<ArticleVersionCreateDto> Articles { get; set; } = new();
        public List<ParametreVersionCreateDto> Parametres { get; set; } = new();
    }
}
