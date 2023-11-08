using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVAPI.Models;

public partial class UserMenu
{
    [Key]
    [Column(Order = 1)]
    public int MenuId { get; set; }

    [Key]
    [Column(Order = 2)]
    public int UserId { get; set; }

    public short CanAdd { get; set; }

    public short CanEdit { get; set; }

    public short CanDelete { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
