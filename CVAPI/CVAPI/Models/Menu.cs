using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string? MenuName { get; set; }

    public int? ParentMenuId { get; set; }

    public int? Sort { get; set; }

    public short IsMobile { get; set; }

    public short MobileDefault { get; set; }

    public short IsBrowser { get; set; }

    public short BrowserDefault { get; set; }

    public string? Icon { get; set; }

    public short IsTransaction { get; set; }

    public short Status { get; set; }

    public short IsDelete { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
