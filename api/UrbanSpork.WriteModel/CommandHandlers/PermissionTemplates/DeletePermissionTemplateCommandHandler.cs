using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.WriteModel.Commands.PermissionTemplates;

namespace UrbanSpork.WriteModel.CommandHandlers.PermissionTemplates
{
    public class DeletePermissionTemplateCommandHandler : ICommandHandler<DeletePermissionTemplateCommand, string>
    {
        private readonly UrbanDbContext _context;

        public DeletePermissionTemplateCommandHandler(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeletePermissionTemplateCommand command)
        {
            PermissionTemplateProjection template = await _context.PermissionTemplateProjection.SingleOrDefaultAsync(a => a.Id == command.Input.Id);

            if (template != null)
            {
                _context.PermissionTemplateProjection.Remove(template);
                await _context.SaveChangesAsync();
                return $"Template \"{template.Name}\" was successfully Removed.";
            }

            return $"Template was not successfully Removed.";
        }
    }
}
