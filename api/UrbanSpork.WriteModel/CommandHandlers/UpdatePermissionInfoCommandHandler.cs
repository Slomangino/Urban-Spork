using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class UpdatePermissionInfoCommandHandler : ICommandHandler<UpdatePermissionInfoCommand, PermissionDTO>
    {
        private readonly IPermissionManager _permissionManager;

        public UpdatePermissionInfoCommandHandler(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        public async Task<PermissionDTO> Handle(UpdatePermissionInfoCommand command)
        {
            var result = await _permissionManager.UpdatePermissionInfo(command.Input);
            return result;
        }
    }
}
