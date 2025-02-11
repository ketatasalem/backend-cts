using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class CentresDeCharge
{
    public string Code { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual ICollection<Employe> Employes { get; set; } = new List<Employe>();

    public virtual ICollection<PostesDeCharge> PostesDeCharges { get; set; } = new List<PostesDeCharge>();
}
