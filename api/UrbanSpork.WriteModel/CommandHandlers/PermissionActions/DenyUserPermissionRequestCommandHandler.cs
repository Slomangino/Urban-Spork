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
using UrbanSpork.DataAccess.Emails;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;
using UrbanSpork.WriteModel.Commands;
using UrbanSpork.WriteModel.Commands.PermissionActions;

namespace UrbanSpork.WriteModel.CommandHandlers.PermissionActions
{
    public class DenyUserPermissionRequestCommandHandler : ICommandHandler<DenyUserPermissionRequestCommand, UserDTO>
    {
        private readonly IEmail _email;
        private readonly IMapper _mapper;
        private readonly ISession _session;
        
        public DenyUserPermissionRequestCommandHandler(IEmail email, IMapper mapper, ISession session)
        {
            _email = email;
            _mapper = mapper;
            _session = session;
        }

        public async Task<UserDTO> Handle(DenyUserPermissionRequestCommand command)
        {
            var forAgg = await _session.Get<UserAggregate>(command.Input.ForId);
            var byAgg = await _session.Get<UserAggregate>(command.Input.ById);

            command.Input.PermissionsToDeny = VerifyActions(
                forAgg,
                byAgg,
                command.Input.PermissionsToDeny,
                new List<string>
                {
                    typeof(UserPermissionRevokedEvent).FullName,
                    typeof(UserPermissionRequestDeniedEvent).FullName,
                    typeof(UserPermissionGrantedEvent).FullName
                });
            if (command.Input.PermissionsToDeny.Any())
            {
                var permissionAggregates = new List<PermissionAggregate>();
                foreach (var request in command.Input.PermissionsToDeny)
                {
                    permissionAggregates.Add(await _session.Get<PermissionAggregate>(request.Key));
                }
                forAgg.DenyPermissionRequest(byAgg, permissionAggregates, command.Input);
                await _session.Commit();
                _email.SendRequestDeniedMessage(forAgg, permissionAggregates);
            }
            return _mapper.Map<UserDTO>(forAgg);
        }

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
