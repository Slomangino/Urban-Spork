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
    public class EnablePermissionCommandHandler : ICommandHandler<EnablePermissionCommand, PermissionDTO>
    {
        private readonly IPermissionManager _permissionManager;

        public EnablePermissionCommandHandler(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        public async Task<PermissionDTO> Handle(EnablePermissionCommand command)
        {
            var result = await _permissionManager.EnablePermission(command.Input);
            return result;
        }
    }
}
