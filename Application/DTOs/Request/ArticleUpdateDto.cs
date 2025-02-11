using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.DTOs.Request
{
    public class ArticleUpdateDto
    {
        public string? UniteAnalyse { get; set; }
        public string? ArticleDependance { get; set; }
        public decimal? Coefficient1 { get; set; }
        public decimal? Coefficient2 { get; set; }
        public string? Formule { get; set; }
        public string? MethodeAnalyse { get; set; }
        public bool? EstRatio { get; set; }
        public int? ValeurRatioMinimale { get; set; }
        public int? ValeurRatioMaximale { get; set; }
        public int? SeuilMinimum { get; set; }
        public List<RatioArticleCreateDto> RatioArticles { get; set; } = new();
    }
}
