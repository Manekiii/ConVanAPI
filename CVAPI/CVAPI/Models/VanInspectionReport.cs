using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class VanInspectionReport
{
    public int Id { get; set; }

    public int? Containervanid { get; set; }

    public string? Virno { get; set; }

    public int? Size { get; set; }

    public string? Type { get; set; }

    public int? Statusid { get; set; }

    public DateTime? Initialdate { get; set; }

    public string? Truckersname { get; set; }

    public string? Platenumber { get; set; }

    public string? Driver { get; set; }

    public string? Initialremarks { get; set; }

    public int? Initialinspectoruserid { get; set; }

    public DateTime? Initialinspectiondate { get; set; }

    public short? Hasinitial { get; set; }

    public string? Finaltruckersname { get; set; }

    public string? Finalplatenumber { get; set; }

    public string? Finaldriver { get; set; }

    public DateTime? Finaldate { get; set; }

    public string? Shipper { get; set; }

    public string? Customer { get; set; }

    public string? Location { get; set; }

    public int? Colorcodingid { get; set; }

    public string? Finalremarks { get; set; }

    public int? Confirmedbyuserid { get; set; }

    public DateTime? Confirmeddate { get; set; }

    public short? Hasfinal { get; set; }

    public int? Createdbyuserid { get; set; }

    public DateTime? Createddate { get; set; }

    public int? Modifiedbyuserid { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual CvColorCoding? Colorcoding { get; set; }

    public virtual ContainerVan? Containervan { get; set; }

    public virtual CvStatus? Status { get; set; }

    public virtual ICollection<VanInspectionChecklist> VanInspectionChecklists { get; set; } = new List<VanInspectionChecklist>();
}
