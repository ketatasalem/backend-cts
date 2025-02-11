using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class Depot
{
    public string Code { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public string CodeSite { get; set; } = null!;

    public string EstActif { get; set; } = null!;

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Site CodeSiteNavigation { get; set; } = null!;

    public virtual ICollection<Emplacement> Emplacements { get; set; } = new List<Emplacement>();

    public virtual ICollection<EntetesMouvementsStock> EntetesMouvementsStocks { get; set; } = new List<EntetesMouvementsStock>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
