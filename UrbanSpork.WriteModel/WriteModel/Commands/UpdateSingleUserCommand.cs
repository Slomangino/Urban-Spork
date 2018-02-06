using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.WriteModel;

namespace UrbanSpork.WriteModel.WriteModel.Commands
{
    public class UpdateSingleUserCommand : ICommand<UserDTO>
    {
        public UserDTO user;

        public UpdateSingleUserCommand(UserDTO User)
        {
            user = User;
        }
    }
}
