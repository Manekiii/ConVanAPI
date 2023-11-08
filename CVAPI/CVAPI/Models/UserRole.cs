using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public short IsDelete { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
