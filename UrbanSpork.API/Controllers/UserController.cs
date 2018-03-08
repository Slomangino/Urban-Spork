using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.WriteModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;
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

        [HttpGet("{Id}")]
        public async Task<UserDTO> Get(Guid id)
        {
            var message = new GetUserByIdQuery(id);
            var result = await _queryProcessor.Process(message);
            return result;
        }

        [HttpGet]
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var result = _queryProcessor.Process(query);
            return  await result;
        }

        [HttpGet("getusercollection")]
        public async Task<List<UserDTO>> GetUserCollection([FromQuery]UserFilterCriteria filterCriteria)
        {
            var query = new GetUserCollectionQuery
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

        [HttpPost("createuser")]
        public async Task<UserDTO> CreateUser([FromBody] CreateUserInputDTO input)
        {
            var message = new CreateSingleUserCommand(input);
            var result = await _commandDispatcher.Execute(message);
            return result;
        }

        [HttpPut("update/{Id}")]
        public async Task<UserDTO> UpdateUser([FromBody] UpdateUserInformationDTO input, Guid id)
        {
            var message = new UpdateSingleUserCommand(id, input);
            var result = await _commandDispatcher.Execute(message);
            return result;
        }

        [HttpPut("requestPermissions")]
        public async Task<UserDTO> RequestPermissions([FromBody] RequestUserPermissionsDTO input)
        {
            var command = new UserPermissionsRequestedCommand(input);
            var result = await _commandDispatcher.Execute(command);
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

        [HttpPut("denyPermission")]
        public async Task<UserDTO> DenyPermission([FromBody] DenyUserPermissionRequestDTO input)
        {
            var command = new DenyUserPermissionRequestCommand(input);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }

        [HttpPut("grantPermission")]
        public async Task<UserDTO> GrantPermission([FromBody] GrantUserPermissionDTO input)
        {
            var command = new GrantUserPermissionCommand(input);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }

        [HttpPut("revokePermission")]
        public async Task<UserDTO> RevokePermission([FromBody] RevokeUserPermissionDTO input)
        {
            var command = new RevokeUserPermissionCommand(input);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }
    }
}
