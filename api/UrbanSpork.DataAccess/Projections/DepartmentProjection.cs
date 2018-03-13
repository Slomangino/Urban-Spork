using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UrbanSpork.DataAccess.Projections
{
    public class DepartmentProjection
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        
    }
}
