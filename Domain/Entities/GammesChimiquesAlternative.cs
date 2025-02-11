using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class GammesChimiquesAlternative
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public DateTime DateDebutValidite { get; set; }

    public DateTime? DateFinValidité { get; set; }

    public bool EstParDefaut { get; set; }

    public int CodeBain { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public string? NomBain { get; set; }

    public virtual Bain CodeBainNavigation { get; set; } = null!;

    public virtual ICollection<GammesChimiquesVersion> GammesChimiquesVersions { get; set; } = new List<GammesChimiquesVersion>();
}
