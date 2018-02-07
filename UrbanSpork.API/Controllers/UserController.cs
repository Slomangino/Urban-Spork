using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.ReadModel;
using UrbanSpork.CQRS.Interfaces.WriteModel;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.WriteModel;
using UrbanSpork.WriteModel.Commands;
using UrbanSpork.WriteModel.WriteModel.Commands;

namespace UrbanSpork.API.Controllers

//"Host = 127.0.0.1; Username=iceman371;Password=password;Database=democqrs"
/*
 * implement some sort of payload to return data
 * 
 */

{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // private readonly UserRepository _userRepository;
        private readonly IQueryProcessor _queryProcessor;

        private readonly ICommandDispatcher _commandDispatcher;

        //public UserController(UserRepository userRepository)
        public UserController(IQueryProcessor queryProcessor, ICommandDispatcher commandDispatcher)
        {
            // _userRepository = new UserRepository(new Npgsql.NpgsqlConnection("Host=urbansporkdb.cj0fybtxusp9.us-east-1.rds.amazonaws.com;Port=5405;User Id=yamnel;Password=urbansporkpass;Database=urbansporkdb"));
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
            // var tableName = "users";
            var query = new GetAllUsersQuery();
            var result = _queryProcessor.Process(query);
            return  await result;
        }

       
        [HttpPost("create")]
        public async Task<UserDTO> CreateUser([FromBody] UserInputDTO input)
        {
            var message = new CreateSingleUserCommand(input);
            var result = await _commandDispatcher.Execute(message);
            return result;
        }

        [HttpPut("update")]
        public async Task<UserDTO> UpdateUser([FromBody] UserInputDTO input)
        {
            var message = new UpdateSingleUserCommand(input);
            var result = await _commandDispatcher.Execute(message);
            return result;
        }
    }
}
