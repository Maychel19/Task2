using Microsoft.EntityFrameworkCore;
using StudyCaseAGIT.Models;

namespace StudyCaseAGIT.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        public DbSet<ProductionPlan> ProductionPlans { get; set; }
        public DbSet<ProductionAdjustment> ProductionAdjustments { get; set; }
    }
}
