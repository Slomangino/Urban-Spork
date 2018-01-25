using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.Domain.ReadModel.QueryCommands;
using UrbanSpork.Domain.Interfaces.ReadModel;
using UrbanSpork.Domain.Interfaces.WriteModel;
using UrbanSpork.Domain.WriteModel.Commands;

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
        public async Task<UserDTO> Get(int id)
        {
            var message = new GetUserByIdQuery(id);
            var result = await _queryProcessor.Process(message);
            return result;

            //var message = new CreateSingleUserCommand(id);
            //var result = _commandDispatcher.Execute(message);
            //return result;
        }

        //[HttpGet]
        //public async Task<List<UserDTO>> GetAllUsers()
        //{
        //    // var tableName = "users";
        //    var query = new GetAllUsersQuery();
        //    var result = _queryProcessor.Process(query);
        //    return await result;
        //}

        /*[HttpGet]
        public async Task<List<UserDTO>> GetAllUsers()
        {
            // var tableName = "users";
            var query = new GetAllUsersQuery();
            var result = _queryProcessor.Process(query);
            return await result;
        }*/

        [HttpPost]
        public void CreateUser(UserDTO input)
        {
            var message = new CreateSingleUserCommand(input);
            _commandDispatcher.Execute(message);
        }

        //[HttpGet]
        //public async Task<IActionResult> Get(){
        //    var users = await _userRepository.GetAll();
        //    if (users == null)
        //    {
        //        return BadRequest("No users found.");
        //    }
        //    return Ok(users);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id) 
        //{
        //    var user = await _userRepository.GetById(id);

        //    if (user == null)
        //    {
        //        return BadRequest("No user was found with ID " + id.ToString() + ".");
        //    }


        //    return Ok(user);

        //    //return $"value {id}";
        //}
    }
}
