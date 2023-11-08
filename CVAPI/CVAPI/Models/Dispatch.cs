using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Dispatch
{
    public int Id { get; set; }

    public int ContainerVanId { get; set; }

    public string TruckersName { get; set; } = null!;

    public string Plate { get; set; } = null!;

    public string Driver { get; set; } = null!;

    public string? EmptyLoad { get; set; }

    public string? Destination { get; set; }

    public string? Remarks { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ContainerVan ContainerVan { get; set; } = null!;
}
