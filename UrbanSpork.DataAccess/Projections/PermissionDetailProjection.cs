using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;

namespace UrbanSpork.DataAccess.Projections
{
    public class PermissionDetailProjection : IProjection
    {
        public readonly UrbanDbContext _context;

        public PermissionDetailProjection() { }

        public PermissionDetailProjection(UrbanDbContext context)
        {
            _context = context;
        }

        [Key]
        public Guid PermissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime DateCreated { get; set; }

        public async Task ListenForEvents(IEvent @event)
        {
            PermissionDetailProjection perm = new PermissionDetailProjection();
            switch (@event)
            {
                case PermissionCreatedEvent pc:
                    perm.PermissionId = pc.Id;
                    perm.Name = pc.Name;
                    perm.Description = pc.Description;
                    perm.IsActive = pc.IsActive;
                    perm.DateCreated = pc.TimeStamp;

                    _context.PermissionDetailProjection.Add(perm);
                    break;
            }
            
            await _context.SaveChangesAsync();
        }
    }
}
