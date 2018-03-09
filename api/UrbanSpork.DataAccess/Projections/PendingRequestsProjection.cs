using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess.Projections
{
    public class PendingRequestsProjection : IProjection
    {
        private readonly UrbanDbContext _context;

        public PendingRequestsProjection() { }

        public PendingRequestsProjection(UrbanDbContext context)
        {
            _context = context;
        }

        public Guid PermissionId { get; set; }
        public Guid ForId { get; set; }
        public Guid ById { get; set; }
        public string ForFirstName { get; set; }
        public string ForLastName { get; set; }
        public string ForFullName { get; set; }
        public string ByFirstName { get; set; }
        public string ByLastName { get; set; }
        public string ByFullName { get; set; }
        public string RequestType { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime DateOfRequest { get; set; }

        public async Task ListenForEvents(IEvent @event)
        {
            switch (@event)
            {
                case UserPermissionsRequestedEvent upr:
                    foreach (var r in upr.Requests)
                    {
                        var row = new PendingRequestsProjection
                        {
                            PermissionId = r.Key,
                            ForId = r.Value.RequestedFor,
                            ById = r.Value.RequestedBy,               
                            RequestType = "Requested Permission",
                            DateOfRequest = r.Value.RequestDate
                        };

                        row.ByFirstName = await _context.UserDetailProjection.Where(a => a.UserId == row.ById).Select(p => p.FirstName).SingleOrDefaultAsync();
                        row.ByLastName = await _context.UserDetailProjection.Where(a => a.UserId == row.ById).Select(p => p.LastName).SingleOrDefaultAsync();
                        row.ByFullName = row.ByFirstName + " " + row.ByLastName;
                        row.ForFirstName = await _context.UserDetailProjection.Where(a => a.UserId == row.ForId).Select(p => p.FirstName).SingleOrDefaultAsync();
                        row.ForLastName = await _context.UserDetailProjection.Where(a => a.UserId == row.ForId).Select(p => p.LastName).SingleOrDefaultAsync();
                        row.ForFullName = row.ForFirstName + " " + row.ForLastName;


                        //if a request of that type does not already exist for that user and permission add it to the list
                        if (!await _context.PendingRequestsProjection.AnyAsync(a =>
                            a.PermissionId == row.PermissionId && a.ForId == row.ForId && a.RequestType == row.RequestType))
                        {
                            await _context.PendingRequestsProjection.AddAsync(row);
                        }
                    }
                    break;
                case PermissionDiabledEvent pd:
                    var list = _context.PendingRequestsProjection.Where(a => a.PermissionId == pd.Id);
                    _context.PendingRequestsProjection.RemoveRange(list);
                    break;
                case UserPermissionRequestDeniedEvent pd:
                    var reqList = _context.PendingRequestsProjection.Where(a => a.ForId == pd.ForId && pd.PermissionsToDeny.ContainsKey(a.PermissionId));
                    if (reqList.Any())
                    {
                        foreach (var r in reqList)
                        {
                            _context.PendingRequestsProjection.Remove(r);
                        }
                    }
                    break;
                case UserPermissionGrantedEvent pg:
                    var requests = _context.PendingRequestsProjection.Where(a => a.ForId == pg.ForId && pg.PermissionsToGrant.ContainsKey(a.PermissionId));
                    if (requests.Any())
                    {
                        foreach (var r in requests)
                        {
                            _context.PendingRequestsProjection.Remove(r);
                        }
                    }
                    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}
