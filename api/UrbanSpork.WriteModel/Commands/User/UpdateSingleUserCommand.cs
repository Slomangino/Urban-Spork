using System;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.WriteModel.Commands.User
{
    public class UpdateSingleUserCommand : ICommand<UpdateUserInformationDTO>
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
