using System;
using System.Collections.Generic;

namespace CVAPI.Models;

public partial class Token
{
    public int TokenId { get; set; }

    public int UserId { get; set; }

    public string AuthToken { get; set; } = null!;

    public DateTime IssuedOn { get; set; }

    public DateTime ExpiresOn { get; set; }

    public short IsExpire { get; set; }
}
