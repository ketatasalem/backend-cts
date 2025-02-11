using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class EntetesDemandesInterventionsAnalysis
{
    public int Id { get; set; }

    public string CodeDemandeInterventionAnalyse { get; set; } = null!;

    public int CodeBain { get; set; }

    public string? Responsable { get; set; }

    public string? ExecutePar { get; set; }

    public string? CommenataireResponsable { get; set; }

    public string? CommentaireExecutePar { get; set; }

    public string? CommentaireSuperviseur { get; set; }

    public string Statut { get; set; } = null!;

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Bain CodeBainNavigation { get; set; } = null!;

    public virtual ICollection<LignesDemandesInterventionsAnalysis> LignesDemandesInterventionsAnalyses { get; set; } = new List<LignesDemandesInterventionsAnalysis>();
}
