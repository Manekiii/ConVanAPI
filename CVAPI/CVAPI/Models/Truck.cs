using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Truck
{
    public int Id { get; set; }

    public string PlateNo { get; set; } = null!;

    public string TruckerId { get; set; } = null!;

    public int ChecklistId { get; set; }

    public string VehicleType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public short Status { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int ModifiedByUserId { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual Trucker Trucker { get; set; } = null!;
}
