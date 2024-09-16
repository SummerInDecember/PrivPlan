using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privplan_api.Models
{
    public class AllowedAccounts
    {
        public int Id { get; set; } = 1;
        public string UserName { get; set; } = String.Empty;
        public string Team { get; set; } = String.Empty;
    }
}