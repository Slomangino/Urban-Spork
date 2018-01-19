using System;
using UrbanSpork.Domain.DataTransfer;
using UrbanSpork.Domain.SLCQRS.WriteModel;

namespace UrbanSpork.Domain.WriteModel.Commands
{
    public class CreateSingleUserCommand : ICommand<UserDTO>
    {
        string _input { get; set; }

        public CreateSingleUserCommand(string input)
        {
            _input = input;
        }
    }
}
