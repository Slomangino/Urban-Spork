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
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess
{
    public class PermissionManager : IPermissionManager
    {
        private readonly ISession _session;
        private readonly UrbanDbContext _context;
        private readonly IMapper _mapper;

        public PermissionManager(ISession session, UrbanDbContext context, IMapper mapper)
        {
            _session = session;
            _context = context;
            _mapper = mapper;
        }
        public async Task<PermissionDTO> CreateNewPermission(CreateNewPermissionDTO input)
        {
            var permAgg = PermissionAggregate.CreateNewPermission(input);
            await _session.Add(permAgg);
            await _session.Commit();

            return _mapper.Map<PermissionDTO>(permAgg);
        }

        public async Task<PermissionDTO> UpdatePermissionInfo(UpdatePermissionInfoDTO input)
        {
            var permAgg = await _session.Get<PermissionAggregate>(input.Id);
            permAgg.UpdatePermissionInfo(input);
            await _session.Commit();
            return _mapper.Map<PermissionDTO>(permAgg);
        }

        /// <summary>
        /// 
        /// disabling a permission is a resource intensive action because it explicitly revokes this permission from all
        /// users that have a record of it in their permission list.
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PermissionDTO> DisablePermission(DisablePermissionInputDTO dto)
        {
            var permAgg = await _session.Get<PermissionAggregate>(dto.PermissionId);

            //only fire the event if the permission is already active, i dont think we want 
            //to have a bunch of events disabling a disabled permission
            if (permAgg.IsActive)
            {
                permAgg.DisablePermission(await _session.Get<UserAggregate>(dto.ById));
                await _session.Commit();

                //if the permission was deactivated, explicitly revoke permissions of all user entities that have this permission
                if (!permAgg.IsActive)
                {
                    /*
                     * Explicitly revoke for each user that has requested, been granted, or been denied a permission
                     * when the permission has been disabled, should take caution when allowing a permission to be disabled
                     * HORRIBLY INEFICIENT AND IS CLOGGING UP OUR EVENT STORE NEEDLESSLY
                     */
                    //get list of entities that have this permission
                    var userList = _context.UserDetailProjection.Where(a =>
                        JsonConvert.DeserializeObject<Dictionary<Guid, PermissionDetails>>(a.PermissionList)
                            .ContainsKey(dto.PermissionId));
                    if (userList.Any())
                    {
                        foreach (var user in userList)
                        {
                            //get the entities
                            var userAgg = await _session.Get<UserAggregate>(user.UserId);
                            //revoke the permission through each aggregate, using a new dto
                            //this will circumvent the usermanager and each aggregate will revoke its own access
                            userAgg.RevokePermission(userAgg, new RevokeUserPermissionDTO
                            {
                                ForId = userAgg.Id,
                                ById = userAgg.Id,
                                PermissionsToRevoke = new Dictionary<Guid, PermissionDetails>
                                {
                                    { dto.PermissionId, new PermissionDetails { Reason = "Permission was Disabled." } }
                                }
                            });
                            await _session.Commit();
                        }
                    }
                }
            }
            return _mapper.Map<PermissionDTO>(permAgg);
        }

        public async Task<PermissionDTO> EnablePermission(EnablePermissionInputDTO dto)
        {
            var permAgg = await _session.Get<PermissionAggregate>(dto.PermissionId);

            if (!permAgg.IsActive)
            {
                permAgg.EnablePermission(await _session.Get<UserAggregate>(dto.ById));
                await _session.Commit();
            }
            return _mapper.Map<PermissionDTO>(permAgg);
        }
    }
}
