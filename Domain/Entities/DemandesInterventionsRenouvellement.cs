using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class DemandesInterventionsRenouvellement
{
    public string Code { get; set; } = null!;

    public int? CodeBain { get; set; }

    public DateTime DateHeureLancement { get; set; }

    public DateTime? DateHeureExecution { get; set; }

    public string Statut { get; set; } = null!;

    public string Responsable { get; set; } = null!;

    public string? CommentaireResponsable { get; set; }

    public string? ExecutePar { get; set; }

    public DateTime? ExeDat { get; set; }

    public string? CommentaireExecutePar { get; set; }

    public string? CommentaireSuperviseur { get; set; }

    public int IdPlanDemandeIntervention { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Bain? CodeBainNavigation { get; set; }
}
