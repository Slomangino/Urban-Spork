using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetPermissionByIdQueryHandler : IQueryHandler<GetPermissionByIdQuery, PermissionDTO>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionByIdQueryHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public Task<PermissionDTO> Handle(GetPermissionByIdQuery query)
        {
            return _permissionRepository.GetById(query.Id);
        }
    }
}
