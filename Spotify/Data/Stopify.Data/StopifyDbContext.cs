namespace Stopify.Data
{
    public class StopifyDbContext : IdentityDbContext<User>
    {
        public StopifyDbContext(DbContextOptions<StopifyDbContext> options)
            : base(options)
        {
        }
    }
}
