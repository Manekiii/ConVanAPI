using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Procured
{
    public int Id { get; set; }

    public string? Trucking { get; set; }

    public string? PlateNumber { get; set; }

    public string? Driver { get; set; }

    public string? ShippingLinesForPullout { get; set; }

    public string? IssuedBy { get; set; }

    public int? WithAtw { get; set; }

    public string? Outbound { get; set; }

    public string? Dsnumber { get; set; }

    public string? Cvnumber { get; set; }

    public string? NotedBy { get; set; }

    public string? Cy { get; set; }

    public string? AcknowledgeBy { get; set; }

    public short Status { get; set; }

    public short IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
