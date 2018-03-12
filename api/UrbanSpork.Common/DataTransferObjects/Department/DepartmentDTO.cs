using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.Department
{
    public class DepartmentDTO
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
