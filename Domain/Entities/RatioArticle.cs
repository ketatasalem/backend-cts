using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class RatioArticle
{
    public int Id { get; set; }

    public decimal? ValeurMesure { get; set; }

    public decimal? ValeurRatio { get; set; }

    public string? CodeArticle { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Article? CodeArticleNavigation { get; set; }
}
