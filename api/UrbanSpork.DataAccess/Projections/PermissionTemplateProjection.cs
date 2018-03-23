using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Projections
{
    public class PermissionTemplateProjection : IProjection
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "json")]
        public string TemplatePermissions { get; set; }
        

        public Task ListenForEvents(IEvent @event)
        {
            //listen for permission named changed, 
            //permission disabled also needs to remove that permission from the template
            throw new NotImplementedException();
        }
    }
}
