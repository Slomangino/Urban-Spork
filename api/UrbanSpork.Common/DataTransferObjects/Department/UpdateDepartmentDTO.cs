using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.Department
{
    public class UpdateDepartmentDTO
    {
        public Guid Id { get; set; }
        public Guid UpdatedById { get; set; }
        public string Name { get; set; }
    }
}
