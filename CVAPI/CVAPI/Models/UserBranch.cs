using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class UserBranch
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public int BranchId { get; set; }

    public short Status { get; set; }

    public long? CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
