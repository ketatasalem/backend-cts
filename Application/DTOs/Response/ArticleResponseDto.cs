using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Domain.Entities;

namespace Labo_Cts_backend.Application.DTOs.Response
{
    public class ArticleResponseDto
    {
        public string Code { get; set; } = null!;
        public string Categorie { get; set; } = null!;
        public string Designation { get; set; } = null!;
        public string CleRecherche { get; set; } = null!;
        public string? Statut { get; set; }
        public string? UniteStock { get; set; }
        public string? UniteConditionnement { get; set; }
        public decimal? ValeurConversionUniteStockUniteCond { get; set; }
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
        public virtual Article? ArticleDependanceNavigation { get; set; }
        public virtual ICollection<RatioArticleResponseDto> RatioArticles { get; set; } = new List<RatioArticleResponseDto>();
    }
}
