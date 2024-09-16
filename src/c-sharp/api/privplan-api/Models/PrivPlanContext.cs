using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace privplan_api.Models
{
    public class PrivPlanContext : DbContext
    {
        public PrivPlanContext(DbContextOptions<PrivPlanContext> options) : base(options)
        {

        }

        public DbSet<AllowedAccounts> AllowedAccounts { get; set; } = null!;
    }
}