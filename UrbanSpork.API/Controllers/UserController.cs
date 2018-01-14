using System;
using Microsoft.AspNetCore.Mvc;
using UrbanSpork.Domain.ReadModel.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSpork.Domain.SLCQRS.ReadModel;
using UrbanSpork.Domain.ReadModel.QueryCommands;

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
        private readonly QueryProcessor _queryProcessor;

        //public UserController(UserRepository userRepository)
        public UserController(QueryProcessor queryProcessor)
        {
            // _userRepository = new UserRepository(new Npgsql.NpgsqlConnection("Host=urbansporkdb.cj0fybtxusp9.us-east-1.rds.amazonaws.com;Port=5405;User Id=yamnel;Password=urbansporkpass;Database=urbansporkdb"));
            _queryProcessor = queryProcessor;
        }

        [HttpGet]
        public string Get()
        {
            var message = new GetAllUsersQuery();
            var result = _queryProcessor.Process(message);
            return "derp";
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
