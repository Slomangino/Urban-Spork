using Microsoft.EntityFrameworkCore;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.DataAccess.DataAccess
{
    public class UrbanDbContext : DbContext
    {
        public UrbanDbContext(){}

        public UrbanDbContext(DbContextOptions<UrbanDbContext> options) : base(options)
        {

        }

        public DbSet<UserDetailProjection> UserDetailProjection {get; set;}
        //public DbSet<User> Users { get; set; }
        public DbSet<EventStoreDataRow> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventStoreDataRow>()
                .HasKey(a => new { a.Id, a.Version });
        }
    }
}
