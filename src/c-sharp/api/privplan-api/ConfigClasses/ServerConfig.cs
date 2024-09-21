using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privplan_api.ConfigClasses
{
    public class ServerConfig
    {
        public string HashedPassword { get; set; } = String.Empty;
        public string Salt {get; set;} = String.Empty;
    }
}