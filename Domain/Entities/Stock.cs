using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class Stock
{
    public string CodeArticle { get; set; } = null!;

    public int CodeEmplacement { get; set; }

    public string CodeDepot { get; set; } = null!;

    public int Quantite { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Article CodeArticleNavigation { get; set; } = null!;

    public virtual Depot CodeDepotNavigation { get; set; } = null!;

    public virtual Emplacement CodeEmplacementNavigation { get; set; } = null!;
}
