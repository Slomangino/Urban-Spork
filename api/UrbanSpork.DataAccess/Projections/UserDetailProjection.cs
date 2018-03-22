using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UrbanSpork.Common;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess.Projections
{
    public class UserDetailProjection : IProjection
    {
        private readonly UrbanDbContext _context;

        public UserDetailProjection()
        {
        }

        public UserDetailProjection(UrbanDbContext context)
        {
            _context = context;
        }

        [Key] public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        [Column(TypeName = "json")] public string PermissionList { get; set; }
        [Column(TypeName = "timestamp")] public DateTime DateCreated { get; set; }

        public async Task ListenForEvents(IEvent @event)
        {
            UserDetailProjection user = new UserDetailProjection();
            Dictionary<Guid, DetailedUserPermissionInfo> permissionList =
                new Dictionary<Guid, DetailedUserPermissionInfo>();
            switch (@event)
            {
                case UserCreatedEvent uc:
                    user.UserId = uc.Id;
                    user.FirstName = uc.FirstName;
                    user.LastName = uc.LastName;
                    user.Email = uc.Email;
                    user.Position = uc.Position;
                    user.Department = uc.Department;
                    user.IsActive = uc.IsActive;
                    user.IsAdmin = uc.IsAdmin;
                    if (uc.PermissionList != null)
                    {
                        foreach (var permission in uc.PermissionList)
                        {
                            var userDetail = await _context.UserDetailProjection
                                .Where(a => a.UserId == permission.Value.RequestedBy).SingleAsync();
                            permissionList.Add(permission.Key,
                                new DetailedUserPermissionInfo
                                {
                                    PermissionName =
                                        await _context.PermissionDetailProjection
                                            .Where(a => a.PermissionId == permission.Key).Select(p => p.Name)
                                            .SingleAsync(),
                                    ApproverFirstName = userDetail.FirstName,
                                    ApproverLastName = userDetail.LastName,
                                    PermissionStatus = "Granted",
                                });
                        }
                    }

                    user.PermissionList = JsonConvert.SerializeObject(permissionList);
                    user.DateCreated = uc.TimeStamp;
                    _context.UserDetailProjection.Add(user);
                    break;

                case UserUpdatedEvent uu:
                    user = await _context.UserDetailProjection.SingleAsync(b => b.UserId == uu.Id);
                    _context.UserDetailProjection.Attach(user);
                    user.FirstName = uu.FirstName;
                    user.LastName = uu.LastName;
                    user.Email = uu.Email;
                    user.Position = uu.Position;
                    user.Department = uu.Department;
                    user.IsAdmin = uu.IsAdmin;
                    _context.Entry(user).Property(a => a.FirstName).IsModified = true;
                    _context.Entry(user).Property(a => a.LastName).IsModified = true;
                    _context.Entry(user).Property(a => a.Email).IsModified = true;
                    _context.Entry(user).Property(a => a.Position).IsModified = true;
                    _context.Entry(user).Property(a => a.Department).IsModified = true;
                    _context.Entry(user).Property(a => a.IsAdmin).IsModified = true;
                    _context.UserDetailProjection.Update(user);
                    break;

                case UserDisabledEvent ud:
                    user = await _context.UserDetailProjection.SingleAsync(b => b.UserId == ud.Id);
                    _context.UserDetailProjection.Attach(user);
                    user.IsActive = ud.IsActive;
                    _context.Entry(user).Property(a => a.IsActive).IsModified = true;
                    _context.UserDetailProjection.Update(user);
                    break;

                case UserEnabledEvent ue:
                    user = await _context.UserDetailProjection.SingleAsync(b => b.UserId == ue.Id);
                    _context.UserDetailProjection.Attach(user);
                    user.IsActive = ue.IsActive;
                    _context.Entry(user).Property(a => a.IsActive).IsModified = true;
                    _context.UserDetailProjection.Update(user);
                    break;

                case UserPermissionsRequestedEvent upr:
                    user = await _context.UserDetailProjection.SingleAsync(b => b.UserId == upr.Id);
                    _context.UserDetailProjection.Attach(user);
                    permissionList =
                        JsonConvert.DeserializeObject<Dictionary<Guid, DetailedUserPermissionInfo>>(
                            user.PermissionList);
                    foreach (var permission in upr.Requests)
                    {
                        if (!permissionList.ContainsKey(permission.Key))
                        {
                            var userDetail = await _context.UserDetailProjection
                                .Where(a => a.UserId == permission.Value.RequestedBy).SingleAsync();
                            permissionList.Add(permission.Key,
                                new DetailedUserPermissionInfo
                                {
                                    PermissionName =
                                        await _context.PermissionDetailProjection
                                            .Where(a => a.PermissionId == permission.Key).Select(p => p.Name)
                                            .SingleAsync(),
                                    ApproverFirstName = userDetail.FirstName,
                                    ApproverLastName = userDetail.LastName,
                                    PermissionStatus = "Requested",
                                    TimeStamp = upr.TimeStamp,
                                });
                        }
                        else
                        {
                            permissionList[permission.Key].PermissionStatus = "Requested";
                            permissionList[permission.Key].TimeStamp = upr.TimeStamp;
                        }
                    }

                    user.PermissionList = JsonConvert.SerializeObject(permissionList);
                    _context.Entry(user).Property(a => a.PermissionList).IsModified = true;
                    _context.UserDetailProjection.Update(user);
                    break;

                case UserPermissionRequestDeniedEvent pde:
                    if (pde.PermissionsToDeny.Any())
                    {
                        user = await _context.UserDetailProjection.SingleAsync(a => a.UserId == pde.ForId);
                        _context.UserDetailProjection.Attach(user);
                        permissionList =
                            JsonConvert.DeserializeObject<Dictionary<Guid, DetailedUserPermissionInfo>>(
                                user.PermissionList);
                        foreach (var permission in pde.PermissionsToDeny)
                        {
                            //Cannot Deny a permission that has not been Requested or has already been Granted
                            if (permissionList.ContainsKey(permission.Key))
                            {
                                permissionList[permission.Key].PermissionStatus = "Denied";
                                permissionList[permission.Key].TimeStamp = pde.TimeStamp;
                            }
                        }

                        user.PermissionList = JsonConvert.SerializeObject(permissionList);
                        _context.Entry(user).Property(a => a.PermissionList).IsModified = true;
                        _context.UserDetailProjection.Update(user);
                    }
                    break;

                case UserPermissionGrantedEvent pg:
                    if (pg.PermissionsToGrant.Any())
                    {
                        user = await _context.UserDetailProjection.SingleAsync(a => a.UserId == pg.Id);
                        _context.UserDetailProjection.Attach(user);
                        permissionList =
                            JsonConvert.DeserializeObject<Dictionary<Guid, DetailedUserPermissionInfo>>(
                                user.PermissionList);
                        foreach (var permission in pg.PermissionsToGrant)
                        {
                            if (permissionList.ContainsKey(permission.Key))
                            {
                                permissionList[permission.Key].PermissionStatus = "Granted";
                                permissionList[permission.Key].TimeStamp = pg.TimeStamp;
                            }
                            else
                            {
                                var userDetail = await _context.UserDetailProjection
                                    .Where(a => a.UserId == permission.Value.RequestedBy).SingleAsync();
                                permissionList.Add(permission.Key,
                                    new DetailedUserPermissionInfo
                                    {
                                        PermissionName =
                                            await _context.PermissionDetailProjection
                                                .Where(a => a.PermissionId == permission.Key).Select(p => p.Name)
                                                .SingleAsync(),
                                        ApproverFirstName = userDetail.FirstName,
                                        ApproverLastName = userDetail.LastName,
                                        PermissionStatus = "Granted",
                                        TimeStamp = pg.TimeStamp,
                                    });
                            }
                        }
                        user.PermissionList = JsonConvert.SerializeObject(permissionList);
                        _context.Entry(user).Property(a => a.PermissionList).IsModified = true;
                        _context.UserDetailProjection.Update(user);
                    }
                    break;
                case UserPermissionRevokedEvent pr:
                    if (pr.PermissionsToRevoke.Any())
                    {
                        user = await _context.UserDetailProjection.SingleAsync(a => a.UserId == pr.Id);
                        _context.UserDetailProjection.Attach(user);
                        permissionList =
                            JsonConvert.DeserializeObject<Dictionary<Guid, DetailedUserPermissionInfo>>(
                                user.PermissionList);
                        foreach (var permission in pr.PermissionsToRevoke)
                        {
                            //Cannot Deny a permission that has not been Granted
                            if (permissionList.ContainsKey(permission.Key))
                            {
                                permissionList[permission.Key].PermissionStatus = "Revoked";
                                permissionList[permission.Key].TimeStamp = pr.TimeStamp;
                            }
                        }

                        user.PermissionList = JsonConvert.SerializeObject(permissionList);
                        _context.Entry(user).Property(a => a.PermissionList).IsModified = true;
                        _context.UserDetailProjection.Update(user);
                    }
                    break;
                case PermissionInfoUpdatedEvent pInfoUpdatedEvent:
                    if (pInfoUpdatedEvent.Name.Any())
                    {
                        var aAlist = _context.UserDetailProjection.Where(p =>
                            JsonConvert
                                .DeserializeObject<Dictionary<Guid, DetailedUserPermissionInfo>>(p.PermissionList)
                                .ContainsKey(pInfoUpdatedEvent.Id)).ToList();
                        var iDToCheck = pInfoUpdatedEvent.Id;
                        var newName = pInfoUpdatedEvent.Name;
                        foreach (var aActivity in aAlist)
                        {
                            //_context.Entry(aActivity).State = EntityState.Modified;
                            permissionList =
                                JsonConvert.DeserializeObject<Dictionary<Guid, DetailedUserPermissionInfo>>(
                                    aActivity.PermissionList);
                            permissionList[iDToCheck].PermissionName = newName;
                            aActivity.PermissionList = JsonConvert.SerializeObject(permissionList);

                            _context.UserDetailProjection.Update(aActivity);
                        }
                    }
                    break;
            }
            await _context.SaveChangesAsync();
        }
    }
}