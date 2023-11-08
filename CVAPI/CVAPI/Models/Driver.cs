using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Driver
{
    public string Id { get; set; } = null!;

    public string Licenseno { get; set; } = null!;

    public string Truckercode { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Middlename { get; set; } = null!;

    public string? Nickname { get; set; }

    public string? Emailadd { get; set; }

    public string? Contactno { get; set; }

    public short Status { get; set; }

    public string? Imgfilename { get; set; }

    public int? Createdbyuserid { get; set; }

    public DateTime Createddate { get; set; }

    public int? Modifiedbyuserid { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual Trucker TruckercodeNavigation { get; set; } = null!;
}
