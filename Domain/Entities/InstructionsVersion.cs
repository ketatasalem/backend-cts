using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class InstructionsVersion
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public DateTime DateDebutValidite { get; set; }

    public DateTime? DateFinValidite { get; set; }

    public int CodeBain { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual Bain CodeBainNavigation { get; set; } = null!;

    public virtual ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();
}
