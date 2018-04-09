using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess.Projections
{
    public class UserHistoryProjection : IProjection
    {
        private readonly UrbanDbContext _context;

        public UserHistoryProjection() { }

        public UserHistoryProjection(UrbanDbContext context)
        {
            _context = context;
        }

        public Guid UserId { get; set; }
        public int Version { get; set; }
        public string EventType { get; set; }
        public string Operator { get; set; }
        [Column(TypeName = "json")] public string Description { get; set; }
        [Column(TypeName = "timestamp")] public DateTime TimeStamp { get; set; }

        public async Task ListenForEvents(IEvent @event)
        {
            var proj = new UserHistoryProjection();
            switch (@event)
            {
                case UserCreatedEvent uce:

                    proj.UserId = uce.Id;
                    proj.Version = uce.Version;
                    proj.EventType = "User Created";
                    proj.Operator = "System";
                    proj.Description = JsonConvert.SerializeObject(uce);
                    proj.TimeStamp = uce.TimeStamp;

                    _context.UserHistoryProjection.Add(proj);
                    break;

                case UserUpdatedEvent uue:
                    proj.UserId = uue.Id;
                    proj.Version = uue.Version;
                    proj.EventType = "User Information Updated";
                    proj.Operator = "System";
                    proj.Description = Descriptify<UserUpdatedEvent>(uue);
                    proj.TimeStamp = uue.TimeStamp;

                    _context.UserHistoryProjection.Add(proj);
                    break;

                case UserDisabledEvent ude:
                    proj.UserId = ude.Id;
                    proj.Version = ude.Version;
                    proj.EventType = "User Disabled";
                    proj.Operator = await _context.UserDetailProjection.Where(a => a.UserId == ude.ByAgg)
                        .Select(a => a.FirstName + " " + a.LastName).FirstOrDefaultAsync();
                    proj.Description = "User was Disabled.";
                    proj.TimeStamp = ude.TimeStamp;

                    _context.UserHistoryProjection.Add(proj);
                    break;

                case UserEnabledEvent uee:
                    proj.UserId = uee.Id;
                    proj.Version = uee.Version;
                    proj.EventType = "User Enabled";
                    proj.Operator = await _context.UserDetailProjection.Where(a => a.UserId == uee.ByAgg)
                        .Select(a => a.FirstName + " " + a.LastName).FirstOrDefaultAsync();
                    proj.Description = "User was Enabled.";
                    proj.TimeStamp = uee.TimeStamp;

                    _context.UserHistoryProjection.Add(proj);
                    break;

                case UserPermissionsRequestedEvent upre:
                    proj.UserId = upre.Id;
                    proj.Version = upre.Version;
                    proj.EventType = "Permissions Requested";
                    proj.Operator = await _context.UserDetailProjection.Where(a => a.UserId == upre.Requests.First().Value.RequestedBy)
                        .Select(a => a.FirstName + " " + a.LastName).FirstOrDefaultAsync();
                    proj.Description = await Descriptify<UserPermissionsRequestedEvent>(upre);
                    proj.TimeStamp = upre.TimeStamp;

                    _context.UserHistoryProjection.Add(proj);
                    break;

                case UserPermissionRequestDeniedEvent uprde:
                    proj.UserId = uprde.Id;
                    proj.Version = uprde.Version;
                    proj.EventType = "Permission requests Denied.";
                    proj.Operator = await _context.UserDetailProjection.Where(a => a.UserId == uprde.PermissionsToDeny.First().Value.RequestedBy)
                        .Select(a => a.FirstName + " " + a.LastName).FirstOrDefaultAsync();
                    proj.Description = await Descriptify<UserPermissionRequestDeniedEvent>(uprde);
                    proj.TimeStamp = uprde.TimeStamp;

                    _context.UserHistoryProjection.Add(proj);
                    break;

                case UserPermissionGrantedEvent upge:
                    proj.UserId = upge.Id;
                    proj.Version = upge.Version;
                    proj.EventType = "Permissions Granted";
                    proj.Operator = await _context.UserDetailProjection.Where(a => a.UserId == upge.PermissionsToGrant.First().Value.RequestedBy)
                        .Select(a => a.FirstName + " " + a.LastName).FirstOrDefaultAsync();
                    proj.Description = await Descriptify<UserPermissionGrantedEvent>(upge);
                    proj.TimeStamp = upge.TimeStamp;

                    _context.UserHistoryProjection.Add(proj);
                    break;

                case UserPermissionRevokedEvent upre:
                    proj.UserId = upre.Id;
                    proj.Version = upre.Version;
                    proj.EventType = "Permissions Revoked";
                    proj.Operator = await _context.UserDetailProjection.Where(a => a.UserId == upre.PermissionsToRevoke.First().Value.RequestedBy)
                        .Select(a => a.FirstName + " " + a.LastName).FirstOrDefaultAsync();
                    proj.Description = await Descriptify<UserPermissionRevokedEvent>(upre);
                    proj.TimeStamp = upre.TimeStamp;

                    _context.UserHistoryProjection.Add(proj);
                    break;
            }

           await _context.SaveChangesAsync();
        }

        private string Descriptify<T>(UserUpdatedEvent e) where T : UserUpdatedEvent
        {
            return JsonConvert.SerializeObject(new
            {
                e.FirstName,
                e.LastName,
                e.Email,
                e.Department,
                e.Position,
                e.IsAdmin,
            });
        }

        private async Task<string> Descriptify<T>(UserPermissionsRequestedEvent e) where T : UserPermissionsRequestedEvent
        {
            //get list of permission in the list of requests
            var permissions = await _context.PermissionDetailProjection.Where(a => e.Requests.ContainsKey(a.PermissionId))
                .ToListAsync();
            var dict = new Dictionary<string, string>();
            
            // put them into a dict where key= PermissionName, Value= Reason for Request.
            foreach (var permissionDetailProjection in permissions)
            {
                dict.Add(permissionDetailProjection.Name, e.Requests[permissionDetailProjection.PermissionId].Reason);
            }

            return JsonConvert.SerializeObject(dict);
        }

        private async Task<string> Descriptify<T>(UserPermissionRequestDeniedEvent e) where T : UserPermissionRequestDeniedEvent
        {
            //get list of permission in the list of Denied Requests
            var permissions = await _context.PermissionDetailProjection.Where(a => e.PermissionsToDeny.ContainsKey(a.PermissionId))
                .ToListAsync();
            var dict = new Dictionary<string, string>();

            // put them into a dict where key= PermissionName, Value= Reason for Denial.
            foreach (var permissionDetailProjection in permissions)
            {
                dict.Add(permissionDetailProjection.Name, e.PermissionsToDeny[permissionDetailProjection.PermissionId].Reason);
            }

            return JsonConvert.SerializeObject(dict);
        }

        private async Task<string> Descriptify<T>(UserPermissionGrantedEvent e) where T : UserPermissionGrantedEvent
        {
            //get list of permission in the list of Granted Permissions
            var permissions = await _context.PermissionDetailProjection.Where(a => e.PermissionsToGrant.ContainsKey(a.PermissionId))
                .ToListAsync();
            var dict = new Dictionary<string, string>();

            // put them into a dict where key= PermissionName, Value= Reason for Granting.
            foreach (var permissionDetailProjection in permissions)
            {
                dict.Add(permissionDetailProjection.Name, e.PermissionsToGrant[permissionDetailProjection.PermissionId].Reason);
            }

            return JsonConvert.SerializeObject(dict);
        }

        private async Task<string> Descriptify<T>(UserPermissionRevokedEvent e) where T : UserPermissionRevokedEvent
        {
            //get list of permission in the list of Revoked Permissions
            var permissions = await _context.PermissionDetailProjection.Where(a => e.PermissionsToRevoke.ContainsKey(a.PermissionId))
                .ToListAsync();
            var dict = new Dictionary<string, string>();

            // put them into a dict where key= PermissionName, Value= Reason for Revocation .
            foreach (var permissionDetailProjection in permissions)
            {
                dict.Add(permissionDetailProjection.Name, e.PermissionsToRevoke[permissionDetailProjection.PermissionId].Reason);
            }

            return JsonConvert.SerializeObject(dict);
        }
    }
}
