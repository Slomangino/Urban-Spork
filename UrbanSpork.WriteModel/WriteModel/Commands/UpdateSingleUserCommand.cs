using CQRSLite.WriteModel.Command;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.WriteModel.WriteModel.Commands
{
    public class UpdateSingleUserCommand : ICommand<UserDTO>
    {
        public Guid id;
        public UserInputDTO userInputDTO;

        public UpdateSingleUserCommand(Guid id, UserInputDTO User)
        {
            this.id = id;
            userInputDTO = User;
        }
    }
}
