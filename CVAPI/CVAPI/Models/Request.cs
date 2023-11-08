using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Request
{
    public int Id { get; set; }

    public string? PlateNumber { get; set; }

    public string? Driver { get; set; }

    public string? EmptyLoad { get; set; }

    public string? Destination { get; set; }

    public int? Size { get; set; }

    public string? ShippingLine { get; set; }

    public string? Dispatcher { get; set; }

    public string? Customer { get; set; }

    public DateTimeOffset? CallTime { get; set; }

    public string? Remarks { get; set; }

    public string? ContainerVanNumber { get; set; }

    public string? VanInspector { get; set; }

    public short Status { get; set; }

    public short IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
