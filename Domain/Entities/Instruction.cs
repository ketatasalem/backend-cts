using System;
using System.Collections.Generic;

namespace Labo_Cts_backend.Domain.Entities;

public partial class Instruction
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int IdInstructionVersion { get; set; }

    public DateTime CreDat { get; set; }

    public string CreUsr { get; set; } = null!;

    public DateTime? UpdDat { get; set; }

    public string? UpdUsr { get; set; }

    public virtual InstructionsVersion IdInstructionVersionNavigation { get; set; } = null!;
}
