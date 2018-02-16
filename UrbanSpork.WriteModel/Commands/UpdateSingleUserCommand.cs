using UrbanSpork.CQRS.WriteModel.Command;
using System;
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
