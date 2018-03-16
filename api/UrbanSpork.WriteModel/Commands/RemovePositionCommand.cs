using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.WriteModel.Commands
{
    public class RemovePositionCommand : ICommand<PositionProjection>
    {
        public Guid id;

        public RemovePositionCommand(Guid ID)
        {
            id = ID;
        }
    }
}
