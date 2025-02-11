using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class PostesDeCharge
{
    public string Code { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public string CodeSite { get; set; } = null!;

    public string CodeCentreCharge { get; set; } = null!;

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual CentresDeCharge CodeCentreChargeNavigation { get; set; } = null!;

    public virtual Site CodeSiteNavigation { get; set; } = null!;
}
