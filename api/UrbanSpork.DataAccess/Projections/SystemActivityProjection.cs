using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess.Projections
{
    public class SystemActivityProjection : IProjection
    {
        private readonly UrbanDbContext _context;

        public SystemActivityProjection() { }

        public SystemActivityProjection(UrbanDbContext context)
        {
            _context = context;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid ForId { get; set; }
        public Guid ById { get; set; }
        public string ForFullName { get; set; }
        public string ByFullName { get; set; }
        public Guid PermissionId { get; set; }
        public string EventType { get; set; }
        public string PermissionName { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime Timestamp { get; set; }

        public async Task ListenForEvents(IEvent @event)
        {
            SystemActivityProjection proj = new SystemActivityProjection();
            switch (@event)
            {
                case UserPermissionGrantedEvent pg:
                    foreach (var permission in pg.PermissionsToGrant)
                    {
                        //US-075
                        var newGrantedRow = new SystemActivityProjection
                        {
                            ForId = pg.ForId,
                            ById = pg.ById,
                            PermissionId = permission.Key,
                            EventType = "Permission Granted",
                            Timestamp = pg.TimeStamp,
                            PermissionName = await _context.PermissionDetailProjection.Where(a => a.PermissionId == permission.Key).Select(p => p.Name).SingleOrDefaultAsync(),
                            ForFullName = await _context.UserDetailProjection.Where(a => a.UserId == pg.ForId).Select(p => p.FirstName + " " + p.LastName).SingleOrDefaultAsync(),
                            ByFullName = await _context.UserDetailProjection.Where(a => a.UserId == pg.ById).Select(p => p.FirstName + " " + p.LastName).SingleOrDefaultAsync()
                        };

                        _context.SystemActivityProjection.Add(newGrantedRow);
                    }
                    break;

                case UserPermissionRevokedEvent pr:
                    foreach (var permission in pr.PermissionsToRevoke)
                    {
                        var newRevokedRow = new SystemActivityProjection
                        {
                            ForId = pr.ForId,
                            ById = pr.ById,
                            PermissionId = permission.Key,
                            EventType = "Permission Revoked",
                            Timestamp = pr.TimeStamp,
                            PermissionName = await _context.PermissionDetailProjection.Where(a => a.PermissionId == permission.Key).Select(p => p.Name).SingleOrDefaultAsync(),
                            ForFullName = await _context.UserDetailProjection.Where(a => a.UserId == pr.ForId).Select(p => p.FirstName + " " + p.LastName).SingleOrDefaultAsync()
                        };
                        if (pr.ById != Guid.Empty)
                        {
                            newRevokedRow.ByFullName = await _context.UserDetailProjection.Where(a => a.UserId == pr.ById).Select(p => p.FirstName + " " + p.LastName).SingleOrDefaultAsync();
                        }
                        else
                        {
                            newRevokedRow.ByFullName = "System";
                        }

                        await _context.SystemActivityProjection.AddAsync(newRevokedRow);
                    }
                    break;

                case UserUpdatedEvent uu:
                    var entries = await _context.SystemActivityProjection.Where(a => a.ForId == uu.Id || a.ById == uu.Id).ToListAsync();
                    if (entries.Any())
                    {
                        foreach (var item in entries)
                        {
                            _context.SystemActivityProjection.Attach(item);

                            if (item.ById == uu.Id)
                            {
                                item.ByFullName = uu.FirstName + " " + uu.LastName;
                                _context.Entry(item).Property(a => a.ByFullName).IsModified = true;
                            }

                            if (item.ForId == uu.Id)
                            {
                                item.ForFullName = uu.FirstName + " " + uu.LastName;
                                _context.Entry(item).Property(a => a.ForFullName).IsModified = true;
                            }

                            _context.SystemActivityProjection.Update(item);
                        }
                    }
                    break;

                case PermissionInfoUpdatedEvent pi:
                    var rows = await _context.SystemActivityProjection.Where(a => a.PermissionId == pi.Id).ToListAsync();
                    if (rows.Any())
                    {
                        foreach (var row in rows)
                        {
                            row.PermissionName = pi.Name;

                            _context.Entry(row).Property(a => a.PermissionName).IsModified = true;
                            _context.SystemActivityProjection.Update(row);
                        }
                    }
                    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}
