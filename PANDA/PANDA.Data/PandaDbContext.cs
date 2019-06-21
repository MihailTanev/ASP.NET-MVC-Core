namespace PANDA.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PANDA.Models;

    public class PandaDbContext : IdentityDbContext<PandaUser>
    {
        public PandaDbContext(DbContextOptions<PandaDbContext> options)
            : base(options)
        {
        }
    }
}
