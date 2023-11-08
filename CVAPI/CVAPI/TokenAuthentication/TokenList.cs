using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVAPI.TokenAuthentication
{
    public class TokenList
    {

        public string? validtoken { get; set; }
        public DateTime expirydate { get; set; }
    }
}
