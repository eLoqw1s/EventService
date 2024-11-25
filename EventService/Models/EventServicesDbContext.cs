using Microsoft.EntityFrameworkCore;

namespace EventService.Models
{
    public class EventServicesDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<MemorableDate> MemorableDates { get; set; }
        public DbSet<New> News { get; set; }
        public EventServicesDbContext(DbContextOptions<EventServicesDbContext> options)
            :base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
