using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class Article
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

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Article? ArticleDependanceNavigation { get; set; }

    public virtual ICollection<ArticlesVersion> ArticlesVersions { get; set; } = new List<ArticlesVersion>();

    public virtual ICollection<Article> InverseArticleDependanceNavigation { get; set; } = new List<Article>();

    public virtual ICollection<LignesMouvementsStock> LignesMouvementsStocks { get; set; } = new List<LignesMouvementsStock>();

    public virtual ICollection<RatioArticle> RatioArticles { get; set; } = new List<RatioArticle>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
