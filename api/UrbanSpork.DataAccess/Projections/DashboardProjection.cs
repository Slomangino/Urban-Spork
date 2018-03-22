using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;

namespace UrbanSpork.DataAccess.Projections
{
    public class DashboardProjection : IProjection
    {
        private readonly UrbanDbContext _context;

        public DashboardProjection() { }

        public DashboardProjection(UrbanDbContext context)
        {
            _context = context;
        }

        [Key]
        public Guid Id { get; set; }
        public Guid PermissionId { get; set; }
        public int ActiveUsers { get; set; }
        public int PendingRequests { get; set; }
        public string SystemName { get; set; }
        public string LogoUrl { get; set; }

        public async Task ListenForEvents(IEvent @event)
        {
            DashboardProjection dBProjection = new DashboardProjection();

            switch (@event)
            {
                case PermissionInfoUpdatedEvent pIUEvent:
                    dBProjection = await _context.DashBoardProjection.SingleAsync(a => a.PermissionId == pIUEvent.Id);
                    _context.Attach(dBProjection);

                    dBProjection.SystemName = pIUEvent.Name;
                    //dBProjection.LogoUrl = pIUEvent.Url;
                    _context.Entry(dBProjection).Property(a => a.SystemName).IsModified = true;
                    //_context.Entry(dBProjection).Property(a => a.LogoUrl).IsModified = true;

                    _context.DashBoardProjection.Update(dBProjection);
                    break;
            }
            await _context.SaveChangesAsync();
        }
    }
}