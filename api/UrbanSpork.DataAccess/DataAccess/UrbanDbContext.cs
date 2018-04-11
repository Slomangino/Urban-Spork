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

        public virtual DbSet<EventStoreDataRow> Events { get; set; }


        /*
         * Whenever there is a projection added, Navigate to UrbanSpork.API.Controllers.EventController and add that projections table name
         * ie: PendingRequestsProjection to the DropAllProjectionData() method in this format:
         *
         * await _context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE MyTableNameHere");
         *
         * This ensures that when rebuilding projections, all data was previously deleted.
         */

        public virtual DbSet<UserDetailProjection> UserDetailProjection { get; set; }
        public virtual DbSet<PermissionDetailProjection> PermissionDetailProjection { get; set; }
        public virtual DbSet<PendingRequestsProjection> PendingRequestsProjection { get; set; }
        public virtual DbSet<SystemDropdownProjection> SystemDropDownProjection { get; set; }
        public virtual DbSet<UserManagementProjection> UserManagementProjection { get; set; }
        public virtual DbSet<ApproverActivityProjection> ApproverActivityProjection { get; set; }
        public virtual DbSet<SystemActivityProjection> SystemActivityProjection { get; set; }
        public virtual DbSet<DashboardProjection> DashBoardProjection { get; set; }
        public virtual DbSet<DepartmentProjection> DepartmentProjection { get; set; } // Do not add to EventController
        public virtual DbSet<PositionProjection> PositionProjection { get; set; } // Do not add to EventController
        public virtual DbSet<PermissionTemplateProjection> PermissionTemplateProjection { get; set; } // Do not add to EventController
        public virtual DbSet<UserHistoryProjection> UserHistoryProjection { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventStoreDataRow>()
                .HasKey(a => new { a.Id, a.Version });
            modelBuilder.Entity<PendingRequestsProjection>()
                .HasKey(a => new {a.PermissionId, a.ForId, a.RequestType});
            modelBuilder.Entity<UserHistoryProjection>()
                .HasKey(a => new {a.UserId, a.Version});

            //https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL/issues/279
            modelBuilder.Model.GetEntityTypes()
                .Select(a => a.Relational())
                .ToList()
                .ForEach(t => t.TableName = t.TableName.ToLower());
        }
    }
}
