using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class ApproverActivityProjection : IProjection
    {
        private readonly UrbanDbContext _context;

        public ApproverActivityProjection() { }

        public ApproverActivityProjection(UrbanDbContext context)
        {
            _context = context;
        }

        [Key]
        public Guid Id { get; set; }
        public Guid ApproverId { get; set; }
        public string ApproverFullName { get; set; }
        public Guid ForId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ForFullName { get; set; }
        public string PermissionName { get; set; }
        public string Reason { get; set; }
        public string TruncatedEventType { get; set; }
      
        

        public async Task ListenForEvents(IEvent @event)
        {
            switch (@event)
            {
                case UserCreatedEvent userCreated:
                    if (userCreated.PermissionList.Any())
                    {
                        foreach (var permission in userCreated.PermissionList)
                        {
                            var appAct = new ApproverActivityProjection();
                            if (userCreated.ById == Guid.Empty)
                            {
                                appAct = new ApproverActivityProjection
                                {
                                    ApproverId = userCreated.ById,
                                    ApproverFullName = "ADMIN",
                                    TimeStamp = userCreated.TimeStamp,
                                    PermissionName = "NO PERMISSIONS SHOULD BE HERE!!!",
                                    TruncatedEventType = "User Created with Permissions",
                                };
                            }
                            else
                            {
                                appAct = new ApproverActivityProjection
                                    {
                                        ApproverId = userCreated.ById,
                                        ApproverFullName = await _context.UserDetailProjection.Where(a => a.UserId == userCreated.ById).Select(a => a.FirstName + " " + a.LastName).SingleOrDefaultAsync(),
                                        TimeStamp = userCreated.TimeStamp,
                                        PermissionName = await _context.PermissionDetailProjection.Where(p => p.PermissionId == permission.Key).Select(n => n.Name).SingleOrDefaultAsync(),
                                        TruncatedEventType = "User Created with Permissions",
                                    };
                            }
                            
                            await _context.ApproverActivityProjection.AddAsync(appAct);
                        }
                        
                    }
                    break;
                case PermissionInfoUpdatedEvent permissionInfoUpdated:
                    if(!(permissionInfoUpdated.Id == Guid.Empty))
                    {
                        var approverActivity = new ApproverActivityProjection
                        {
                            ApproverId = permissionInfoUpdated.UpdatedById,
                            ApproverFullName = await _context.UserDetailProjection.Where(a => a.UserId == permissionInfoUpdated.UpdatedById).Select(a => a.FirstName + " " + a.LastName).SingleOrDefaultAsync(),
                            TimeStamp = permissionInfoUpdated.TimeStamp,
                            PermissionName = await _context.PermissionDetailProjection.Where(p => p.PermissionId == permissionInfoUpdated.Id).Select(n => n.Name).SingleOrDefaultAsync(),
                            TruncatedEventType = "Updated Permission",
                        };

                        await _context.ApproverActivityProjection.AddAsync(approverActivity);
                    }
                    break;

                case UserPermissionGrantedEvent permissionGranted:
                    foreach(var permission in permissionGranted.PermissionsToGrant)
                    {
                        var PermissionID = permission.Key;
                        var approverActivity = new ApproverActivityProjection
                        {
                            ApproverId = permissionGranted.ById,
                            ApproverFullName = await _context.UserDetailProjection.Where(a => a.UserId == permissionGranted.ById).Select(a => a.FirstName + " " + a.LastName).SingleOrDefaultAsync(),
                            ForId = permissionGranted.Id,
                            TimeStamp = permissionGranted.TimeStamp,
                            PermissionName = await _context.PermissionDetailProjection.Where(p => p.PermissionId == PermissionID).Select(n => n.Name).SingleOrDefaultAsync(),
                            TruncatedEventType = "Granted Permission",
                            Reason = permission.Value.Reason,

                        };
                        approverActivity.ForFullName = await _context.UserDetailProjection.Where(udp => udp.UserId == permissionGranted.ForId).Select(r => r.FirstName +" " + r.LastName).SingleOrDefaultAsync();

                        await _context.ApproverActivityProjection.AddAsync(approverActivity);
                    }
                    break;

                case UserPermissionRevokedEvent permissionRevoked:
                   foreach(var permission in permissionRevoked.PermissionsToRevoke)
                    {
                        var PermissionID = permission.Key;
                        var approverActivity = new ApproverActivityProjection
                        {
                            ApproverId = permissionRevoked.ById,
                            ApproverFullName = await _context.UserDetailProjection.Where(a => a.UserId == permissionRevoked.ById).Select(a => a.FirstName + " " + a.LastName).SingleOrDefaultAsync(),
                            ForId = permissionRevoked.Id,
                            TimeStamp = permissionRevoked.TimeStamp,
                            PermissionName = await _context.PermissionDetailProjection.Where(p => p.PermissionId == PermissionID).Select(n => n.Name).SingleOrDefaultAsync(),
                            TruncatedEventType = "Revoked Permission",
                            Reason = permission.Value.Reason,
                        };
                        approverActivity.ForFullName = await _context.UserDetailProjection.Where(udp => udp.UserId == permissionRevoked.ForId).Select(r => r.FirstName + " " + r.LastName).SingleOrDefaultAsync();

                        await _context.ApproverActivityProjection.AddAsync(approverActivity);
                    }
                    break;

                case UserPermissionRequestDeniedEvent permissionDenied:
                    foreach (var permission in permissionDenied.PermissionsToDeny)
                    {
                        var PermissionID = permission.Key;
                        var approverActivity = new ApproverActivityProjection
                        {
                            ApproverId = permissionDenied.ById,
                            ApproverFullName = await _context.UserDetailProjection.Where(a => a.UserId == permissionDenied.ById).Select(a => a.FirstName + " " + a.LastName).SingleOrDefaultAsync(),
                            ForId = permissionDenied.Id,
                            TimeStamp = permissionDenied.TimeStamp,
                            PermissionName = await _context.PermissionDetailProjection.Where(p => p.PermissionId == PermissionID).Select(n => n.Name).SingleOrDefaultAsync(),
                            TruncatedEventType = "Denied Permission",
                            Reason = permission.Value.Reason
                        };
                        approverActivity.ForFullName = await _context.UserDetailProjection.Where(udp => udp.UserId == permissionDenied.ForId).Select(r => r.FirstName + " " + r.LastName).SingleOrDefaultAsync();

                        await _context.ApproverActivityProjection.AddAsync(approverActivity);
                    }
                    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}