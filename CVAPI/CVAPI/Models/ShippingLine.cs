using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class ShippingLine
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public int? DayofDetention { get; set; }

    public short? IsActive { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<ContainerVan> ContainerVans { get; set; } = new List<ContainerVan>();
}
