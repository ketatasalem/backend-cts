using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class TablesDiversesDonnee
{
    public int Id { get; set; }

    public int IdTableDiverse { get; set; }

    public string Code { get; set; } = null!;

    public string IntituleCode { get; set; } = null!;

    public string? ValeurColonneAlpha1 { get; set; }

    public string? ValeurColonneAlpha2 { get; set; }

    public int? ValeurColonneNumerique1 { get; set; }

    public int? ValeurColonneNumerique2 { get; set; }

    public string? ValeurColonneDependance { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual TablesDiversesDefinition IdTableDiverseNavigation { get; set; } = null!;
}
