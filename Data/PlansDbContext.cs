using Microsoft.EntityFrameworkCore;
using ASP.Models; 

namespace ASP.Data
{
    public class PlansDbContext : DbContext
    {
        public PlansDbContext(DbContextOptions<PlansDbContext> options) : base(options) { }

        public DbSet<Plan> Plans { get; set; }
    }
}
