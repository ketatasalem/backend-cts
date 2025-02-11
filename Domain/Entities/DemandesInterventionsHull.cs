using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class DemandesInterventionsHull
{
    public string Code { get; set; } = null!;

    public DateTime DateHeureLancement { get; set; }

    public DateTime? DateHeureExecution { get; set; }

    public string Statut { get; set; } = null!;

    public int IdPlanDemandeIntervention { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual PlansDemandesIntervention IdPlanDemandeInterventionNavigation { get; set; } = null!;
}
