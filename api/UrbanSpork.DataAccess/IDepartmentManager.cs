using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Department;

namespace UrbanSpork.DataAccess
{
    public interface IDepartmentManager
    {
        Task<DepartmentDTO> CreateNewDepartmentAsync(CreateDepartmentDTO input);
        Task<DepartmentDTO> UpdateDepartmentAsync(UpdateDepartmentDTO input);
        Task<DepartmentDTO> DisableDepartmentAsync(Guid id);
        Task<DepartmentDTO> EnableDepartmentAsync(Guid id);
    }
}
