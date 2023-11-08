using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Trucker
{
    public string Id { get; set; } = null!;

    public string? Ownername { get; set; }

    public string? Ownertype { get; set; }

    public string? Contactperson { get; set; }

    public string? Telephoneno { get; set; }

    public string? Celphoneno { get; set; }

    public string? Emailadd { get; set; }

    public string? Address { get; set; }

    public string? Remarks { get; set; }

    public string? Branch { get; set; }

    public short? Status { get; set; }

    public string? Imgfilename { get; set; }

    public int? Createdbyuserid { get; set; }

    public DateTime? Createddate { get; set; }

    public int? Modifiedbyuserid { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Truck> Trucks { get; set; } = new List<Truck>();
}
