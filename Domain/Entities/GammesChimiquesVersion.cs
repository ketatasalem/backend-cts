using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class GammesChimiquesVersion
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public DateTime DateDebutValidite { get; set; }

    public DateTime? DateFinValidite { get; set; }

    public int IdGammeAlternative { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual ICollection<ArticlesVersion> ArticlesVersions { get; set; } = new List<ArticlesVersion>();

    public virtual GammesChimiquesAlternative IdGammeAlternativeNavigation { get; set; } = null!;

    public virtual ICollection<ParametresVersion> ParametresVersions { get; set; } = new List<ParametresVersion>();
}
