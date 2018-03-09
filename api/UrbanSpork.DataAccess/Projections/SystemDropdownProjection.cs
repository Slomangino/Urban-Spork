using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;

namespace UrbanSpork.DataAccess.Projections
{
   public class SystemDropdownProjection : IProjection
    {
        private readonly UrbanDbContext _context;

        public SystemDropdownProjection() { }

        public SystemDropdownProjection(UrbanDbContext context)
        {
            _context = context;
        }

        [Key]
        public Guid PermissionID { get; set; }
        public string PermissionName { get; set; }

        public async Task ListenForEvents(IEvent @event)
        {
            SystemDropdownProjection permission = new SystemDropdownProjection();
            switch (@event)
            {
                case PermissionCreatedEvent pc:
                    permission.PermissionID = pc.Id;
                    permission.PermissionName = pc.Name;
                    _context.SystemDropDownProjection.Add(permission);
                    break;
                case PermissionInfoUpdatedEvent pu:
                    permission = await _context.SystemDropDownProjection.SingleAsync(p => p.PermissionID == pu.Id);
                    permission.PermissionID = pu.Id;
                    permission.PermissionName = pu.Name;
                    _context.Update(permission);
                    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}
