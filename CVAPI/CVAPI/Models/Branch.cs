using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Branch
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Alias { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public short Status { get; set; }

    public short IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
}
