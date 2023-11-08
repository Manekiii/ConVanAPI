using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class CvColorCoding
{
    public int Id { get; set; }

    public string ColorName { get; set; } = null!;

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<VanInspectionReport> VanInspectionReports { get; set; } = new List<VanInspectionReport>();
}
