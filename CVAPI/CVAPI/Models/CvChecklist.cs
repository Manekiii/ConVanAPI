using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class CvChecklist
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public short? Types { get; set; }

    public short IsDelete { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByuserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
