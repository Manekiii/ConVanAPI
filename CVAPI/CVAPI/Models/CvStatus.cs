using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class CvStatus
{
    public int Id { get; set; }

    public string Statusname { get; set; } = null!;

    public short Isdelete { get; set; }

    public int? Createdbyuserid { get; set; }

    public DateTime Createddate { get; set; }

    public int? Modifiedbyuserid { get; set; }

    public DateTime? Modifieddate { get; set; }

    public short? Isforempty { get; set; }

    public virtual ICollection<ContainerVan> ContainerVans { get; set; } = new List<ContainerVan>();

    public virtual ICollection<VanInspectionReport> VanInspectionReports { get; set; } = new List<VanInspectionReport>();
}
