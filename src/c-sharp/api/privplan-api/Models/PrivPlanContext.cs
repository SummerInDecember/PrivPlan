using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace privplan_api.Models
{
    public class PrivPlanContext : DbContext
    {
        public PrivPlanContext(DbContextOptions<PrivPlanContext> options) : base(options)
        {

        }

        public DbSet<AllowedDevices> AllowedDevices { get; set; } = null!;
    }
}