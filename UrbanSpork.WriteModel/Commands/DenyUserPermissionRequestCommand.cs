using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
     public class DenyUserPermissionRequestCommand : ICommand<UserDTO>
     {
         public DenyUserPermissionRequestDTO Input;

         public DenyUserPermissionRequestCommand(DenyUserPermissionRequestDTO input)
         {
             this.Input = input;
         }
     }
}
