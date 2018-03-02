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

        public async Task<PermissionDTO> UpdatePermissionInfo(UpdatePermissionInfoDTO input)
        {
            var permAgg = await _session.Get<PermissionAggregate>(input.Id);
            permAgg.UpdatePermissionInfo(input);
            await _session.Commit();
            return Mapper.Map<PermissionDTO>(permAgg);
        }

        public async Task<PermissionDTO> DisablePermission(DisablePermissionDTO input)
        {
            var permAgg = await _session.Get<PermissionAggregate>(input.Id);

            //only fire the event if the permission is already active, i dont think we want 
            //to have a bunch of events disabling a disabled permission
            if (permAgg.IsActive)
            {
                permAgg.DisablePermission(input);
                await _session.Commit();
            }
            return Mapper.Map<PermissionDTO>(permAgg);
        }

        public async Task<PermissionDTO> EnablePermission(EnablePermissionDTO input)
        {
            var permAgg = await _session.Get<PermissionAggregate>(input.Id);

            if (!permAgg.IsActive)
            {
                permAgg.EnablePermission(input);
                await _session.Commit();
            }
            return Mapper.Map<PermissionDTO>(permAgg);
        }
    }
}
