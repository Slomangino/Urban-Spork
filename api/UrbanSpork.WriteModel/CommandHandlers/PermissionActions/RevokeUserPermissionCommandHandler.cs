using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.Emails;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;
using UrbanSpork.WriteModel.Commands.PermissionActions;

namespace UrbanSpork.WriteModel.CommandHandlers.PermissionActions
{
    public class RevokeUserPermissionCommandHandler : ICommandHandler<RevokeUserPermissionCommand, UserDTO>
    {
        private readonly IEmail _email;
        private readonly IMapper _mapper;
        private readonly ISession _session;

        public RevokeUserPermissionCommandHandler(IEmail email, IMapper mapper, ISession session)
        {
            _email = email;
            _mapper = mapper;
            _session = session;
        }

        public async Task<UserDTO> Handle(RevokeUserPermissionCommand command)
        {
            var forAgg = await _session.Get<UserAggregate>(command.Input.ForId);
            var byAgg = await _session.Get<UserAggregate>(command.Input.ById);

            command.Input.PermissionsToRevoke = VerifyActions(
                forAgg,
                byAgg,
                command.Input.PermissionsToRevoke,
                new List<string> {
                    typeof(UserPermissionsRequestedEvent).FullName,
                    typeof(UserPermissionRevokedEvent).FullName,
                    typeof(UserPermissionRequestDeniedEvent).FullName
                });

            if (command.Input.PermissionsToRevoke.Any())
            {
                var permissionAggregates = new List<PermissionAggregate>();
                foreach (var request in command.Input.PermissionsToRevoke)
                {
                    permissionAggregates.Add(await _session.Get<PermissionAggregate>(request.Key));
                }

                forAgg.RevokePermission(byAgg, command.Input);
                await _session.Commit();
                _email.SendPermissionsRevokedMessage(forAgg, permissionAggregates);
            }


            return _mapper.Map<UserDTO>(forAgg);
        }

        /**
         * filters actions taken according to the forAgg's permissionList's event type, and the event types being passed in
         *
         * Ex: if a forAgg's permission list has VisualStudio permission's  eventType as "revoked", we do not want to be able to revoke it again, so we
         * remove it from the list of actions.
         */
        private Dictionary<Guid, PermissionDetails> VerifyActions(UserAggregate forAgg, UserAggregate byAgg, Dictionary<Guid, PermissionDetails> requests, List<string> eventTypesToRemove)
        {
            var result = new Dictionary<Guid, PermissionDetails>();
            var markedForRemoval = new List<Guid>();
            foreach (var request in requests)
            {
                result[request.Key] = request.Value;
                //if forAgg's permission list contains a definition for that permission
                if (forAgg.PermissionList.ContainsKey(request.Key))
                {
                    // if the definition of that permission is one of the eventTypes to remove, remove it from requests.
                    if (eventTypesToRemove.Contains(
                        JsonConvert.DeserializeObject<string>(forAgg.PermissionList[request.Key].EventType)))
                    {
                        markedForRemoval.Add(request.Key);
                    }
                    else
                    {
                        //might not even need this******because admin rights are handled in the aggregate.
                        //(admins can override eventTypes) Otherwise, if they are not an admin, remove it from requests
                        if (!byAgg.IsAdmin)
                        {
                            markedForRemoval.Add(request.Key);
                        }
                    }
                } // if the permission list does not contain, this case will be handled by the aggregate since admins can do 
                  // whatever they want. and the aggregate will restrict user actions based on that
            }

            markedForRemoval.ForEach(a => result.Remove(a));

            return result;
        }
    }
}
