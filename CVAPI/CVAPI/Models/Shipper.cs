using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Shipper
{
    public int Id { get; set; }

    public int Branchid { get; set; }

    public string Customercode { get; set; } = null!;

    public string? Parentcode { get; set; }

    public string? Customername { get; set; }

    public string? Address { get; set; }

    public string? Contact { get; set; }

    public string? Shippercode { get; set; }

    public string? Shipperaddress { get; set; }

    public short? Isactive { get; set; }

    public int? Createdbyuserid { get; set; }

    public DateTime Createddate { get; set; }

    public int? Modifiedbyuserid { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual Branch Branch { get; set; } = null!;
}
