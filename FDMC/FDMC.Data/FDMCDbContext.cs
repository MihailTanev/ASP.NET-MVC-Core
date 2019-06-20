namespace FDMC.Data
{
    using FDMC.Model;
    using Microsoft.EntityFrameworkCore;

    public class FDMCDbContext : DbContext
    {
        public FDMCDbContext(DbContextOptions<FDMCDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }   
}
