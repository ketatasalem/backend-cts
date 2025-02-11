using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class PlansDemandesIntervention
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Intitule { get; set; } = null!;

    public string? Frequence { get; set; }

    public int? Compteur { get; set; }

    public string? CodePosteCharge { get; set; }

    public string? CodeBain { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual ICollection<DemandesInterventionsAnalysis> DemandesInterventionsAnalyses { get; set; } = new List<DemandesInterventionsAnalysis>();

    public virtual ICollection<DemandesInterventionsHull> DemandesInterventionsHulls { get; set; } = new List<DemandesInterventionsHull>();
}
