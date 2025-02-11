using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class Bain
{
    public int Code { get; set; }

    public string Emplacement { get; set; } = null!;

    public int DimensionLargeur { get; set; }

    public int DimensionLongueur { get; set; }

    public int DimensionHauteur { get; set; }

    public bool? EstReference { get; set; }

    public int? BainReferenceCode { get; set; }

    public bool Actif { get; set; }

    public string CodePosteCharge { get; set; } = null!;

    public int? IdVersionGammeChimiqueActif { get; set; }

    public int? IdAlternativeGammeChimiqueActif { get; set; }

    public int? IdInstructionVersionActif { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Bain? BainReferenceCodeNavigation { get; set; }

    public virtual ICollection<DemandesInterventionsRenouvellement> DemandesInterventionsRenouvellements { get; set; } = new List<DemandesInterventionsRenouvellement>();

    public virtual ICollection<EntetesDemandesInterventionsAnalysis> EntetesDemandesInterventionsAnalyses { get; set; } = new List<EntetesDemandesInterventionsAnalysis>();

    public virtual ICollection<EntetesDemandesInterventionsHull> EntetesDemandesInterventionsHulls { get; set; } = new List<EntetesDemandesInterventionsHull>();

    public virtual ICollection<GammesChimiquesAlternative> GammesChimiquesAlternatives { get; set; } = new List<GammesChimiquesAlternative>();

    public virtual ICollection<InstructionsVersion> InstructionsVersions { get; set; } = new List<InstructionsVersion>();

    public virtual ICollection<Bain> InverseBainReferenceCodeNavigation { get; set; } = new List<Bain>();
}
