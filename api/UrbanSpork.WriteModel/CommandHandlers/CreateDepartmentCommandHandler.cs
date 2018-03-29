using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, DepartmentProjection>
    {
        private readonly UrbanDbContext _context;

        public CreateDepartmentCommandHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<DepartmentProjection> Handle(CreateDepartmentCommand command)
        {
            var department = new DepartmentProjection
            {
                Name = command.Input.Name,
            };
            try
            {
                 _context.DepartmentProjection.Add(department);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

            return  department;
        }
    }
}
