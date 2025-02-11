namespace Labo_Cts_backend.Application.DTOs.Response
{
    public class ArticleVersionResponseDto
    {
        public int IdGammeVersion { get; set; }

        public string CodeArticle { get; set; } = null!;

        public decimal? ValeurMin { get; set; }

        public decimal? ValeurMax { get; set; }

        public decimal? Valeur { get; set; }
    }
}
