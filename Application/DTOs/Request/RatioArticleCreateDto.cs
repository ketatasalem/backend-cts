namespace Labo_Cts_backend.Application.DTOs.Request
{
    public class RatioArticleCreateDto
    {
        public int? Id { get; set; }

        public decimal? ValeurMesure { get; set; }

        public decimal? ValeurRatio { get; set; }

        public string? CodeArticle { get; set; }
    }
}
