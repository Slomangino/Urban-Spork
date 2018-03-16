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
    public class RemovePositionCommandHandler : ICommandHandler<RemovePositionCommand, PositionProjection>
    {
        private readonly UrbanDbContext _context;

        public RemovePositionCommandHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<PositionProjection> Handle(RemovePositionCommand command)
        {
            var position = new PositionProjection
            {
                Id = command.id,
            };

            try
            {
                _context.PositionProjection.Remove(position);
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
