using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class EntetesMouvementsStock
{
    public string Code { get; set; } = null!;

    public DateTime DateImputation { get; set; }

    public string TypeMvt { get; set; } = null!;

    public string CodeSite { get; set; } = null!;

    public string CodeDepot { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public string PieceOrigine { get; set; } = null!;

    public string Statut { get; set; } = null!;

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Depot CodeDepotNavigation { get; set; } = null!;

    public virtual Site CodeSiteNavigation { get; set; } = null!;

    public virtual ICollection<LignesMouvementsStock> LignesMouvementsStocks { get; set; } = new List<LignesMouvementsStock>();
}
