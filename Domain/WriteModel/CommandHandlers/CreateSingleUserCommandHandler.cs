using System;
using UrbanSpork.Domain.WriteModel.Commands;
using UrbanSpork.Domain.SLCQRS.WriteModel;
using UrbanSpork.Domain.DataTransfer;

namespace UrbanSpork.Domain.WriteModel
{
    public class CreateSingleUserCommandHandler : ICommandHandler<CreateSingleUserCommand, UserDTO>
    {
        public CreateSingleUserCommandHandler()
        {
        }

        public UserDTO Handle(CreateSingleUserCommand command)
        {
            var result = new UserDTO
            {
                userId = 12345,
                firstName = "Tyler",
                lastName = "Hall",
                position = "Software Engineer",
                department = "Development",
                isActive = true,
            };
            return result;
            //return "CreateSingleUserCommand executed!";
        }
    }
}
