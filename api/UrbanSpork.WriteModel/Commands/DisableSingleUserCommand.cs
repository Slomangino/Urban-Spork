using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class DisableSingleUserCommand : ICommand<UserDTO>
    {
        public Guid Id { get; set; }

        public DisableSingleUserCommand(Guid id)
        {
            this.Id = id;
        }
    }
}
