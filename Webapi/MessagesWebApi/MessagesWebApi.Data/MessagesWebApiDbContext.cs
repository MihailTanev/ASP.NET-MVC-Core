namespace MessagesWebApi.Data
{
    using MessagesWebApi.Models;
    using Microsoft.EntityFrameworkCore;

    public class MessagesWebApiDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public MessagesWebApiDbContext(DbContextOptions options) 
            : base(options)
        {

        }
    }
}
