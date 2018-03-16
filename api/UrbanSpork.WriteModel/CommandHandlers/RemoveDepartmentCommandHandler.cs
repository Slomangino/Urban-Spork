using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class RemoveDepartmentCommandHandler : ICommandHandler<RemoveDepartmentCommand, DepartmentProjection>
    {
        private readonly UrbanDbContext _context;

        public RemoveDepartmentCommandHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<DepartmentProjection> Handle(RemoveDepartmentCommand command)
        {
            var department = new DepartmentProjection
            {
                Id = command.id,
            };

            try
            {
                _context.DepartmentProjection.Remove(department);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }
          

            return department;
        }
    }
}

