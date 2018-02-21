using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.WriteModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.FilterCriteria;
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

        [HttpGet("{id}")]
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


        [HttpPost("createuser")]
        public async Task<UserDTO> CreateUser([FromBody] UserInputDTO input)
        {
            var message = new CreateSingleUserCommand(input);
            var result = await _commandDispatcher.Execute(message);
            return result;
        }

        [HttpPut("update/{id}")]
        public async Task<UserDTO> UpdateUser([FromBody] UserInputDTO input, Guid id)
        {
            var message = new UpdateSingleUserCommand(id, input);
            var result = await _commandDispatcher.Execute(message);
            return result;
        }
    }
}
