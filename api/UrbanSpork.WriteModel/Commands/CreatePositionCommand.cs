using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Position;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.WriteModel.Commands
{
    public class CreatePositionCommand : ICommand<PositionProjection>
    {
        public CreatePositionDTO Input { get; set; }

        public CreatePositionCommand(CreatePositionDTO input)
        {
            Input = input;
        }
    }
}
