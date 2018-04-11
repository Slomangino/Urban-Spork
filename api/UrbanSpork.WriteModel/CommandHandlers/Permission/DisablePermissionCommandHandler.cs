using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.WriteModel.Commands.Permission;

namespace UrbanSpork.WriteModel.CommandHandlers.Permission
{
    public class DisablePermissionCommandHandler : ICommandHandler<DisablePermissionCommand, PermissionDTO>
    {
        private readonly IMapper _mapper;
        private readonly ISession _session;
        private readonly UrbanDbContext _context;

        public DisablePermissionCommandHandler(IMapper mapper, ISession session, UrbanDbContext context)
        {
            _mapper = mapper;
            _session = session;
            _context = context;
        }

        /// <summary>
        /// 
        /// disabling a permission is a resource intensive action because it explicitly revokes this permission from all
        /// users that have a record of it in their permission list.
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<PermissionDTO> Handle(DisablePermissionCommand command)
        {
            //var result = await _permissionManager.DisablePermission(command.Input);
            //return result;

            var permAgg = await _session.Get<PermissionAggregate>(command.Input.PermissionId);

            //only fire the event if the permission is already active, i dont think we want 
            //to have a bunch of events disabling a disabled permission
            if (permAgg.IsActive)
            {
                permAgg.DisablePermission(await _session.Get<UserAggregate>(command.Input.ById));
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
                            .ContainsKey(command.Input.PermissionId));
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
                                    { command.Input.PermissionId, new PermissionDetails { Reason = "Permission was Disabled." } }
                                }
                            });
                            await _session.Commit();
                        }
                    }
                }
            }
            return _mapper.Map<PermissionDTO>(permAgg);
        }
    }
}
