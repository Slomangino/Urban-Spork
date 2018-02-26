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

        public DbSet<EventStoreDataRow> Events { get; set; }

        public DbSet<UserDetailProjection> UserDetailProjection { get; set; }
        public DbSet<PermissionDetailProjection> PermissionDetailProjection { get; set; }
        public DbSet<PendingRequestsProjection> PendingRequestsProjection { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventStoreDataRow>()
                .HasKey(a => new { a.Id, a.Version });
            modelBuilder.Entity<PendingRequestsProjection>()
                .HasKey(a => new {a.PermissionId, a.ForId, a.RequestType});
        }
    }
}
