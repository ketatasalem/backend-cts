using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class Employe
{
    public string Matricule { get; set; } = null!;

    public string NomPrenom { get; set; } = null!;

    public string Fonction { get; set; } = null!;

    public string CodeCentreCharge { get; set; } = null!;

    public string? Email { get; set; }

    public string MotDePasse { get; set; } = null!;

    public string? CompteLdap { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual CentresDeCharge CodeCentreChargeNavigation { get; set; } = null!;
}
