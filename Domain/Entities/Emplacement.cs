using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class Emplacement
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public string EstActif { get; set; } = null!;

    public string CodeDepot { get; set; } = null!;

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Depot CodeDepotNavigation { get; set; } = null!;

    public virtual ICollection<LignesMouvementsStock> LignesMouvementsStockCodeEmplacementDestinationNavigations { get; set; } = new List<LignesMouvementsStock>();

    public virtual ICollection<LignesMouvementsStock> LignesMouvementsStockCodeEmplacementOrigineNavigations { get; set; } = new List<LignesMouvementsStock>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
