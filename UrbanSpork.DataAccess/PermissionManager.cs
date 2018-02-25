using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Domain;

namespace UrbanSpork.DataAccess
{
    public class PermissionManager : IPermissionManager
    {
        private readonly ISession _session;

        public PermissionManager(ISession session)
        {
            _session = session;
        }
        public async Task<PermissionDTO> CreateNewPermission(CreateNewPermissionDTO input)
        {
            var permAgg = PermissionAggregate.CreateNewPermission(input);
            await _session.Add(permAgg);
            await _session.Commit();

            return Mapper.Map<PermissionDTO>(permAgg);

        }
    }
}
