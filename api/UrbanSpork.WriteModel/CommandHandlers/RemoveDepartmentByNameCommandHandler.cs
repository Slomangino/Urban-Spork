using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class RemoveDepartmentByNameCommandHandler : ICommandHandler<RemoveDepartmentByNameCommand, DepartmentProjection>
    {
        private readonly UrbanDbContext _context;

        public RemoveDepartmentByNameCommandHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<DepartmentProjection> Handle(RemoveDepartmentByNameCommand command)
        {
            

             var id = _context.DepartmentProjection.Where(a => a.Name == command.name).Select(a => a.Id).ToListAsync();

            var department = new DepartmentProjection
            {
                Id = id.Result.First(),
                Name = command.name,
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

