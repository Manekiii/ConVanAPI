using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? Userroleid { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public string? Middlename { get; set; }

    public string? Nickname { get; set; }

    public string? Contactnumber { get; set; }

    public string? Address { get; set; }

    public short? Status { get; set; }

    public short? Isdeactivated { get; set; }

    public int? Createdbyuserid { get; set; }

    public DateTime? Createddate { get; set; }

    public int? Modifiedbyuserid { get; set; }

    public DateTime? Modifieddate { get; set; }

    public virtual ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();

    public virtual UserRole? Userrole { get; set; }
}
