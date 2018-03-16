using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UrbanSpork.DataAccess.Projections
{
    public class PositionProjection
    {
        [Key]
        public Guid Id { get; set; }
        public string PositionName { get; set; }
        public string DepartmentName { get; set; }
    }
}
