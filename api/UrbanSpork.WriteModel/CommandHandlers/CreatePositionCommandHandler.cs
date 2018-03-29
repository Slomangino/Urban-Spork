using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class CreatePositionCommandHandler : ICommandHandler<CreatePositionCommand, PositionProjection>
    {
        private readonly UrbanDbContext _context;

        public CreatePositionCommandHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<PositionProjection> Handle(CreatePositionCommand command)
        {
            var position = new PositionProjection
            {
                PositionName = command.Input.PositionName,
                DepartmentName = command.Input.DepartmentName,
            };
            try
            {
                _context.PositionProjection.Add(position);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

            return position;
        }
    }
}
