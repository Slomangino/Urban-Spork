using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Emails;
using UrbanSpork.DataAccess.Events.Users;
using UrbanSpork.WriteModel.Commands.PermissionActions;

namespace UrbanSpork.WriteModel.CommandHandlers.PermissionActions
{
    public class GrantRevokePermissionsCommandHandler : ICommandHandler<GrantRevokePermissionsCommand, UserDTO>
    {
        private readonly UrbanDbContext _context;
        private readonly IEmail _email;
        private readonly ISession _session;
        private readonly IMapper _mapper;


        public GrantRevokePermissionsCommandHandler(IEmail email, IMapper mapper, UrbanDbContext context, ISession session)
        {
            _email = email;
            _context = context;
            _mapper = mapper;
            _session = session;
        }

        public async Task<UserDTO> Handle(GrantRevokePermissionsCommand command)
        {
            var agg = await _session.Get<UserAggregate>(command.Input.ForId);
            var byAgg = await _session.Get<UserAggregate>(command.Input.ById);

            var permissionsToGrant = GetPermissionsToGrant(agg.PermissionList, command.Input.Permissions);
            var permissionsToRevoke = GetPermissionsToRevoke(agg.PermissionList, command.Input.Permissions);
            var grantPermAggs = new List<PermissionAggregate>();
            var revokePermAggs = new List<PermissionAggregate>();
            if (byAgg.IsAdmin)
            {
                if (permissionsToGrant.Any())
                {
                    
                    foreach (var permission in permissionsToGrant)
                    {
                        grantPermAggs.Add(await _session.Get<PermissionAggregate>(permission.Key));
                    }

                    var grantUserPermissionDTO = new GrantUserPermissionDTO
                    {
                        ForId = agg.Id,
                        ById = byAgg.Id,
                        PermissionsToGrant = permissionsToGrant
                    };
                    agg.GrantPermission(byAgg, grantPermAggs, grantUserPermissionDTO);
                }

                if (permissionsToRevoke.Any())
                {
                    
                    foreach (var permission in permissionsToRevoke)
                    {
                        revokePermAggs.Add(await _session.Get<PermissionAggregate>(permission.Key));
                    }

                    var revokeUserPermissionDTO = new RevokeUserPermissionDTO
                    {
                        ForId = agg.Id,
                        ById = byAgg.Id,
                        PermissionsToRevoke = permissionsToRevoke
                    };
                    agg.RevokePermission(byAgg, revokeUserPermissionDTO);
                }

                _email.SendPermissionsUpdatedMessage(agg, revokePermAggs, grantPermAggs);
                await _session.Commit();
            }

            return _mapper.Map<UserAggregate, UserDTO>(await _session.Get<UserAggregate>(agg.Id));
        }

        private Dictionary<Guid, PermissionDetails> GetPermissionsToRevoke(Dictionary<Guid, PermissionDetails> aggPermissions, Dictionary<Guid, PermissionDetails> newPermissions)
        {
            var listToRevoke = new Dictionary<Guid, PermissionDetails>();
            foreach (var p in aggPermissions)
            {
                if (!newPermissions.ContainsKey(p.Key))
                {
                    listToRevoke[p.Key] = p.Value;
                }
            }

            return listToRevoke;
        }

        private Dictionary<Guid, PermissionDetails> GetPermissionsToGrant(
            Dictionary<Guid, PermissionDetails> aggPermissions, Dictionary<Guid, PermissionDetails> newPermissions)
        {
            var listToGrant = new Dictionary<Guid, PermissionDetails>();
            foreach (var p in newPermissions)
            {
                if (aggPermissions.ContainsKey(p.Key) &&
                    JsonConvert.DeserializeObject<string>(aggPermissions[p.Key].EventType) != typeof(UserPermissionGrantedEvent).FullName || !aggPermissions.ContainsKey(p.Key))
                {
                    listToGrant[p.Key] = p.Value;
                }
            }

            return listToGrant;
        }
    }
}
