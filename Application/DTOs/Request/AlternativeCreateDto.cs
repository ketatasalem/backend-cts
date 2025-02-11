namespace Labo_Cts_backend.Application.DTOs.Request
{
    public class AlternativeCreateDto
    {
        public bool EstParDefaut { get; set; }
        public string? NomBain { get; set; }
        public int? CodeBain { get; set; }
        public List<ArticleVersionCreateDto> Articles { get; set; } = new();
        public List<ParametreVersionCreateDto> Parametres { get; set; } = new();
    }
}
