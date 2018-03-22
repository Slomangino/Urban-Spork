using System.Linq;
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


        /*
         * Whenever there is a projection added, Navigate to UrbanSpork.API.Controllers.EventController and add that projections table name
         * ie: PendingRequestsProjection to the DropAllProjectionData() method in this format:
         *
         * await _context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE MyTableNameHere");
         *
         * This ensures that when rebuilding projections, all data was previously deleted.
         */

        public DbSet<UserDetailProjection> UserDetailProjection { get; set; }
        public DbSet<PermissionDetailProjection> PermissionDetailProjection { get; set; }
        public DbSet<PendingRequestsProjection> PendingRequestsProjection { get; set; }
        public DbSet<SystemDropdownProjection> SystemDropDownProjection { get; set; }
        public DbSet<UserManagementProjection> UserManagementProjection { get; set; }
        public DbSet<ApproverActivityProjection> ApproverActivityProjection { get; set; }
        public DbSet<SystemActivityProjection> SystemActivityProjection { get; set; }
        public DbSet<DashboardProjection> DashBoardProjection { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventStoreDataRow>()
                .HasKey(a => new { a.Id, a.Version });
            modelBuilder.Entity<PendingRequestsProjection>()
                .HasKey(a => new {a.PermissionId, a.ForId, a.RequestType});

            //https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL/issues/279
            modelBuilder.Model.GetEntityTypes()
                .Select(a => a.Relational())
                .ToList()
                .ForEach(t => t.TableName = t.TableName.ToLower());
        }
    }
}
