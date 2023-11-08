using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class ContainerVan
{
    public int Id { get; set; }

    public int? Shippinglineid { get; set; }

    public string? Vannumber { get; set; }

    public string? Eirnumber { get; set; }

    public string? Procuredemptycvan { get; set; }

    public int? Size { get; set; }

    public DateTime? Datearrive { get; set; }

    public int? Cvstatusid { get; set; }

    public int? Agefrompulloutdate { get; set; }

    public int? Pulloutweeknumber { get; set; }

    public DateTime? Pulloutdatefrompier { get; set; }

    public int? Dispatchedweeknumber { get; set; }

    public DateTime? Dispatcheddateofvaninspector { get; set; }

    public DateTime? Returndatetopier { get; set; }

    public string? Dispatchedemptycvan { get; set; }

    public string? Remarks { get; set; }

    public short? Hasdetention { get; set; }

    public int? Noofdayswithdetention { get; set; }

    public short? Neardetention { get; set; }

    public DateTime? Detentiontrigger { get; set; }

    public int? Freedays { get; set; }

    public decimal? Detentionfee { get; set; }

    public decimal? Detentioncharge { get; set; }

    public short? Isdispatch { get; set; }

    public int? Createdbyuserid { get; set; }

    public DateTime? Createddate { get; set; }

    public int? Modifiedbyuserid { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual CvStatus? Cvstatus { get; set; }

    public virtual ICollection<Dispatch> Dispatches { get; set; } = new List<Dispatch>();

    public virtual ShippingLine? Shippingline { get; set; }

    public virtual ICollection<VanInspectionReport> VanInspectionReports { get; set; } = new List<VanInspectionReport>();
}
