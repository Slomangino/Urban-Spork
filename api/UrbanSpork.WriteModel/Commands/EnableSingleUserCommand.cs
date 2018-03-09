using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class EnableSingleUserCommand : ICommand<UserDTO>
    {
        public Guid id { get; set; }

        public EnableSingleUserCommand(Guid id)
        {
            this.id = id;
        }
    }
}
