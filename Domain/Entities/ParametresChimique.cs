using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class ParametresChimique
{
    public string Code { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Unite { get; set; } = null!;

    public string TypeValeur { get; set; } = null!;

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual ICollection<ParametresVersion> ParametresVersions { get; set; } = new List<ParametresVersion>();
}
