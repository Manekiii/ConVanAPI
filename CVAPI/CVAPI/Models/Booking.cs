using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Booking
{
    public int Id { get; set; }

    public string? Destination { get; set; }

    public string? Details { get; set; }

    public string? Class { get; set; }

    public string? Type { get; set; }

    public int? Size { get; set; }

    public int? Quantity { get; set; }

    public string? Driver { get; set; }

    public string? PlateNumber { get; set; }

    public int BranchId { get; set; }

    public short Status { get; set; }

    public short IsApproved { get; set; }

    public short IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
