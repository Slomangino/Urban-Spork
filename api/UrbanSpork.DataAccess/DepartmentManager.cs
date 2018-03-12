
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess.DataAccess;

namespace UrbanSpork.DataAccess
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly ISession _session;
        private readonly UrbanDbContext _context;

        public DepartmentManager(ISession session, UrbanDbContext context)
        {
            _session = session;
            _context = context;
        }

        public async Task<DepartmentDTO> CreateNewDepartmentAsync(CreateDepartmentDTO input)
        {
            var DepartmentAgg = DepartmentAggregate.CreateNewDepartment(input);
            await _session.Add(DepartmentAgg);
            await _session.Commit();

            return Mapper.Map<DepartmentDTO>(DepartmentAgg);
        }

        public async Task<DepartmentDTO> DisableDepartmentAsync(Guid id)
        {
            var DepartmentAgg = await _session.Get<DepartmentAggregate>(id);

            if (DepartmentAgg.IsActive)
            {
                DepartmentAgg.DisableDepartment();
                await _session.Commit();
            }
            var result = Mapper.Map<DepartmentDTO>(DepartmentAgg);
            return result;
        }

        public async Task<DepartmentDTO> EnableDepartmentAsync(Guid id)
        {
            var DepartmentAgg = await _session.Get<UserAggregate>(id);

            if (!DepartmentAgg.IsActive)
            {
                DepartmentAgg.EnableSingleUser();
                await _session.Commit();
            }
            var result = Mapper.Map<DepartmentDTO>(DepartmentAgg);
            return result;
        }

        public async Task<DepartmentDTO> UpdateDepartmentAsync(UpdateDepartmentDTO input)
        {
            var DepartmentAgg = await _session.Get<DepartmentAggregate>(input.Id);
            DepartmentAgg.UpdateDepartment(input);
            await _session.Commit();
            return Mapper.Map<DepartmentDTO>(DepartmentAgg);
        }
    }
}
