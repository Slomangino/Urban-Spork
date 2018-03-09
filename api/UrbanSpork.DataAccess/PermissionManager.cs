using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess.DataAccess;

namespace UrbanSpork.DataAccess
{
    public class PermissionManager : IPermissionManager
    {
        private readonly ISession _session;
        private readonly UrbanDbContext _context;

        public PermissionManager(ISession session, UrbanDbContext context)
        {
            _session = session;
            _context = context;
        }
        public async Task<PermissionDTO> CreateNewPermission(CreateNewPermissionDTO input)
        {
            var permAgg = PermissionAggregate.CreateNewPermission(input);
            await _session.Add(permAgg);
            await _session.Commit();

            return Mapper.Map<PermissionDTO>(permAgg);
        }

        public async Task<PermissionDTO> UpdatePermissionInfo(UpdatePermissionInfoDTO input)
        {
            var permAgg = await _session.Get<PermissionAggregate>(input.Id);
            permAgg.UpdatePermissionInfo(input);
            await _session.Commit();
            return Mapper.Map<PermissionDTO>(permAgg);
        }

        public async Task<PermissionDTO> DisablePermission(Guid id)
        {
            var permAgg = await _session.Get<PermissionAggregate>(id);

            //only fire the event if the permission is already active, i dont think we want 
            //to have a bunch of events disabling a disabled permission
            if (permAgg.IsActive)
            {
                permAgg.DisablePermission();
                await _session.Commit();

                /*
                 * Explicitly revoke for each user that has requested, been granted, or been denied a permission
                 * when the permission has been disabled, should take caution when allowing a permission to be disabled
                 */
                //get list of entities that have this permission
                var userList = _context.UserDetailProjection.Where(a =>
                    JsonConvert.DeserializeObject<Dictionary<Guid, PermissionDetails>>(a.PermissionList)
                        .ContainsKey(id));
                if (userList.Any())
                {
                    foreach (var user in userList)
                    {
                        //get the aggregates for those entities
                        var userAgg = await _session.Get<UserAggregate>(user.UserId);
                        //revoke the permission through each aggregate, using a new dto
                        userAgg.RevokePermission(new RevokeUserPermissionDTO
                        {
                            ForId = userAgg.Id,
                            ById = Guid.Empty,
                            PermissionsToRevoke = new Dictionary<Guid, PermissionDetails>
                            {
                                { id, new PermissionDetails { Reason = "Permission was Disabled." } }
                            }
                        });
                        await _session.Commit();
                    }
                }

            }
            return Mapper.Map<PermissionDTO>(permAgg);
        }

        public async Task<PermissionDTO> EnablePermission(Guid id)
        {
            var permAgg = await _session.Get<PermissionAggregate>(id);

            if (!permAgg.IsActive)
            {
                permAgg.EnablePermission();
                await _session.Commit();
            }
            return Mapper.Map<PermissionDTO>(permAgg);
        }
    }
}
