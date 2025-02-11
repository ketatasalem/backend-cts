using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class Site
{
    public string Code { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public string CodeSociete { get; set; } = null!;

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Societe CodeSocieteNavigation { get; set; } = null!;

    public virtual ICollection<Depot> Depots { get; set; } = new List<Depot>();

    public virtual ICollection<EntetesMouvementsStock> EntetesMouvementsStocks { get; set; } = new List<EntetesMouvementsStock>();

    public virtual ICollection<PostesDeCharge> PostesDeCharges { get; set; } = new List<PostesDeCharge>();
}
