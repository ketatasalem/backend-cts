using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class LignesMouvementsStock
{
    public int Id { get; set; }

    public string CodeArticle { get; set; } = null!;

    public string DesignationArticle { get; set; } = null!;

    public int Quantite { get; set; }

    public string Unite { get; set; } = null!;

    public int CodeEmplacementOrigine { get; set; }

    public int? CodeEmplacementDestination { get; set; }

    public string CodeEnteteMouvementStock { get; set; } = null!;

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Article CodeArticleNavigation { get; set; } = null!;

    public virtual Emplacement? CodeEmplacementDestinationNavigation { get; set; }

    public virtual Emplacement CodeEmplacementOrigineNavigation { get; set; } = null!;

    public virtual EntetesMouvementsStock CodeEnteteMouvementStockNavigation { get; set; } = null!;
}
