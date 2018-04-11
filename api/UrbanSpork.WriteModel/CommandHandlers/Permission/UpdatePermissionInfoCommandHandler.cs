using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.WriteModel.Commands.Permission;

namespace UrbanSpork.WriteModel.CommandHandlers.Permission
{
    public class UpdatePermissionInfoCommandHandler : ICommandHandler<UpdatePermissionInfoCommand, PermissionDTO>
    {
        private readonly IMapper _mapper;
        private readonly ISession _session;

        public UpdatePermissionInfoCommandHandler(IMapper mapper, ISession session)
        {
            _mapper = mapper;
            _session = session;
        }

        public async Task<PermissionDTO> Handle(UpdatePermissionInfoCommand command)
        {
            var permAgg = await _session.Get<PermissionAggregate>(command.Input.Id);
            permAgg.UpdatePermissionInfo(command.Input);
            await _session.Commit();
            return _mapper.Map<PermissionDTO>(permAgg);
        }
    }
}
