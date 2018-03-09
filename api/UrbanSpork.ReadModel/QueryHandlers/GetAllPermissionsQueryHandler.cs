using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetAllPermissionsQueryHandler : IQueryHandler<GetAllPermissionsQuery, List<PermissionDTO>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetAllPermissionsQueryHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public Task<List<PermissionDTO>> Handle(GetAllPermissionsQuery query)
        {
            return _permissionRepository.GetAllPermissions();
        }
    }
}
