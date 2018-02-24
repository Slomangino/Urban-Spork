using UrbanSpork.CQRS.WriteModel.Command;
using System;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.WriteModel.WriteModel.Commands
{
    public class UpdateSingleUserCommand : ICommand<UserDTO>
    {
        public Guid Id;
        public UpdateUserInformationDTO Input;

        public UpdateSingleUserCommand(Guid id, UpdateUserInformationDTO input)
        {
            Id = id;
            Input = input;
        }
    }
}
