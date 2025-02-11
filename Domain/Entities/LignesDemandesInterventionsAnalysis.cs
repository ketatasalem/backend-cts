using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class LignesDemandesInterventionsAnalysis
{
    public int Id { get; set; }

    public string? CodeBain { get; set; }

    public string? EmplacementBain { get; set; }

    public string? CodeArticle { get; set; }

    public decimal? Coefficient1 { get; set; }

    public decimal? Coefficient2 { get; set; }

    public decimal? VolumePreleve { get; set; }

    public decimal? Mesure { get; set; }

    public string? UniteMesure { get; set; }

    public decimal? Ajout { get; set; }

    public string? UniteAjout { get; set; }

    public string Etat { get; set; } = null!;

    public int IdEnteteDemandeInterventionAnlayse { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual EntetesDemandesInterventionsAnalysis IdEnteteDemandeInterventionAnlayseNavigation { get; set; } = null!;
}
