using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.WriteModel.Commands;
using UrbanSpork.WriteModel.Commands.Permission;

namespace UrbanSpork.WriteModel.CommandHandlers.Permission
{
    public class CreatePermissionCommandHandler : ICommandHandler<CreatePermissionCommand, PermissionDTO>
    {
        private readonly IMapper _mapper;
        private readonly ISession _session;

        public CreatePermissionCommandHandler(IMapper mapper, ISession session)
        {
            _session = session;
            _mapper = mapper;
        }

        public async Task<PermissionDTO> Handle(CreatePermissionCommand command)
        {
            var permAgg = PermissionAggregate.CreateNewPermission(command.Input);
            await _session.Add(permAgg);
            await _session.Commit();

            return _mapper.Map<PermissionDTO>(permAgg);
        }
    }
}
