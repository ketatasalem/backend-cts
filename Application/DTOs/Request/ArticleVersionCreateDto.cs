namespace Labo_Cts_backend.Application.DTOs.Request
{
    public class ArticleVersionCreateDto
    {
        public string CodeArticle { get; set; } = null!;
        public decimal? ValeurMin { get; set; }
        public decimal? ValeurMax { get; set; }
        public decimal? Valeur { get; set; }
    }
}
