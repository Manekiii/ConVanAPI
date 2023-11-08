using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class VanInspectionChecklist
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public short? Condition { get; set; }

    public string? Remarks { get; set; }

    public int? Virid { get; set; }

    public short IsInital { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual VanInspectionReport? Vir { get; set; }
}
