using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.WriteModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.Projection;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.WriteModel.Commands;
using UrbanSpork.WriteModel.WriteModel.Commands;

namespace UrbanSpork.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandDispatcher _commandDispatcher;

        public UserController(IQueryProcessor queryProcessor, ICommandDispatcher commandDispatcher)
        {
            _queryProcessor = queryProcessor;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet("id/{Id}")]
        public async Task<UserDetailProjectionDTO> Get(Guid id)
        {
            var message = new GetUserDetailByIdQuery(id);
            var result = await _queryProcessor.Process(message);
            return result;
        }

        [HttpGet("getusercollection")]
        public async Task<List<UserDetailProjectionDTO>> GetUserCollection([FromQuery]UserFilterCriteria filterCriteria)
        {
            var query = new GetUserCollectionQuery
            {
                FilterCriteria = filterCriteria,
            };

            var result = _queryProcessor.Process(query);
            return await result;
        }

        [HttpGet("offboard")]
        public async Task<OffBoardUserDTO> GetOffBoardingPermissionsById(Guid id)
        {
            var query = new GetOffboardUserPermissionsQuery(id);
            var result = _queryProcessor.Process(query);
            return await result;
        }

        [HttpGet("getloginusers")]
        public async Task<List<LoginUserDTO>> GetLoginUsers()
        {
            var query = new GetLoginUsersQuery();
            var result = _queryProcessor.Process(query);
            return await result;
        }

        [HttpGet("getapproveractivity")]
        public async Task<List<ApproverActivityProjection>> GetApproverAtivityProjection([FromQuery]ApproverActivityFilterCriteria filterCriteria)
        {
            var query = new GetApproverActicityProjectionQuery
            {
                FilterCriteria = filterCriteria,
            };

            var result = _queryProcessor.Process(query);
            return await result;
        }

        [HttpGet("getusermanagementprojection")]
        public async Task<List<UserManagementDTO>> GetUserList([FromQuery]UserFilterCriteria filter)
        {
            var message = new GetUserManagementProjectionQuery(filter);
            var result = await _queryProcessor.Process(message);
            return result;
        }

        [HttpGet("getSystemActivityProjection")]
        public async Task<List<SystemActivityDTO>> GetSystemActivity([FromQuery] SystemActivityReportFilterCriteria filter)
        {
            var command = new GetSystemActivityReportQuery(filter);
            var result = await _queryProcessor.Process(command);
            return result;
        }

        [HttpPost("createuser")]
        public async Task<UserDTO> CreateUser([FromBody] CreateUserInputDTO input)
        {
            var message = new CreateSingleUserCommand(input);
            var result = await _commandDispatcher.Execute(message);
            return result;
        }

        [HttpPut("update")]
        public async Task<UpdateUserInformationDTO> UpdateUser([FromBody] UpdateUserInformationDTO input)
        {
            var message = new UpdateSingleUserCommand(input.ForID, input);
            var result = await _commandDispatcher.Execute(message);
            return result;
        }

        [HttpPut("disable/{Id}")]
        public async Task<UserDTO> DisableUser(Guid id)
        {
            var command = new DisableSingleUserCommand(id);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }

        [HttpPut("enable/{Id}")]
        public async Task<UserDTO> EnableUser(Guid id)
        {
            var command = new EnableSingleUserCommand(id);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }
        #region User Permission Operations
        [HttpPut("requestPermissions")]
        public async Task<UserDTO> RequestPermissions([FromBody] RequestUserPermissionsDTO input)
        {
            var command = new UserPermissionsRequestedCommand(input);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }

        [HttpPut("denyPermissions")]
        public async Task<UserDTO> DenyPermission([FromBody] DenyUserPermissionRequestDTO input)
        {
            var command = new DenyUserPermissionRequestCommand(input);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }

        [HttpPut("grantPermissions")]
        public async Task<UserDTO> GrantPermission([FromBody] GrantUserPermissionDTO input)
        {
            var command = new GrantUserPermissionCommand(input);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }

        [HttpPut("revokePermissions")]
        public async Task<UserDTO> RevokePermission([FromBody] RevokeUserPermissionDTO input)
        {
            var command = new RevokeUserPermissionCommand(input);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }
        #endregion
    }
}
