using System;
using Microsoft.AspNetCore.Mvc;
using UrbanSpork.Domain.ReadModel.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSpork.Domain.SLCQRS.ReadModel;
using UrbanSpork.Domain.ReadModel.QueryCommands;
using UrbanSpork.Domain.WriteModel.Commands;
using UrbanSpork.Domain.SLCQRS.WriteModel;
using UrbanSpork.Domain.DataTransfer;
using Newtonsoft.Json.Linq;

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

        [HttpGet]
        public UserDTO Get(int id)
        {
            //var message = new GetAllUsersQuery();
            //var result = _queryProcessor.Process(message);
            //return result;

            var message = new CreateSingleUserCommand("string");
            var result = _commandDispatcher.Execute(message);
            return result;
        }

        public Task<List<JObject>> Get()
        {
            var tableName = "users";
            var query = new GetAllUsersQuery(tableName);
            var result = _queryProcessor.Process(query);
            return result;
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
