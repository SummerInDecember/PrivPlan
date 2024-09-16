using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using privplan_api.Models;

namespace privplan_api.Wrappers
{
    public class AllowedAccountsWrapper
    {
        public AllowedAccounts Request { get; set; } = new AllowedAccounts();
        public string Password {get; set;} = String.Empty;
    }
}