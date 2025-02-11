using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class LignesDemandesInterventionsHull
{
    public int Id { get; set; }

    public string? CodeBain { get; set; }

    public string? EmplacementBain { get; set; }

    public string EtatInitial { get; set; } = null!;

    public string? CodeProduit { get; set; }

    public decimal? Coefficient { get; set; }

    public string? EtatFinal { get; set; }

    public decimal? Consomation { get; set; }

    public string? Unite { get; set; }

    public string Etat { get; set; } = null!;

    public int IdEnteteDemandeInterventionHull { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual EntetesDemandesInterventionsHull IdEnteteDemandeInterventionHullNavigation { get; set; } = null!;
}
