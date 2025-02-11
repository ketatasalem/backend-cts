using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class TablesDiversesDefinition
{
    public int Id { get; set; }

    public string Intitule { get; set; } = null!;

    public int? IdTableDependance { get; set; }

    public string? NomColonneAlpha1 { get; set; }

    public string? NomColonneAlpha2 { get; set; }

    public string? NomColonneNumerique1 { get; set; }

    public string? NomColonneNumerique2 { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual TablesDiversesDefinition? IdTableDependanceNavigation { get; set; }

    public virtual ICollection<TablesDiversesDefinition> InverseIdTableDependanceNavigation { get; set; } = new List<TablesDiversesDefinition>();

    public virtual ICollection<TablesDiversesDonnee> TablesDiversesDonnees { get; set; } = new List<TablesDiversesDonnee>();
}
