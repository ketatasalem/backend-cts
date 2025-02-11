using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class ParametresVersion
{
    public int IdGammeVersion { get; set; }

    public string IdParametreChimique { get; set; } = null!;

    public decimal? ValeurMin { get; set; }

    public decimal? ValeurMax { get; set; }

    public decimal? Valeur { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual GammesChimiquesVersion IdGammeVersionNavigation { get; set; } = null!;

    public virtual ParametresChimique IdParametreChimiqueNavigation { get; set; } = null!;
}
