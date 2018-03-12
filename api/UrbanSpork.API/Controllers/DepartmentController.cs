using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.API.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandDispatcher _commandDispatcher;

        public DepartmentController(IQueryProcessor queryProcessor, ICommandDispatcher commandDispatcher)
        {
            _queryProcessor = queryProcessor;
            _commandDispatcher = commandDispatcher;
        }

        //[HttpGet("{Id}")]
        //public async Task<DepartmentDTO> Get(Guid id)
        //{
        //    var query = new GetDepartmentByIdQuery(id);
        //    var result = await _queryProcessor.Process(query);
        //    return result;
        //}

        //[HttpGet]
        //public async Task<List<DepartmentDTO>> GetAllDepartments()
        //{
        //    var query = new GetAllDepartmentsQuery();
        //    var result = await _queryProcessor.Process(query);
        //    return result;
        //}

        [HttpPost("create")]
        public async Task<DepartmentDTO> CreateDepartment([FromBody] CreateDepartmentDTO input)
        {
            var command = new CreateDepartmentCommand(input);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }

        //[HttpPut("update")]
        //public async Task<DepartmentDTO> UpdatePermission([FromBody] UpdateDepartmentDTO input)
        //{
        //    var command = new UpdatePermissionInfoCommand(input);
        //    var result = await _commandDispatcher.Execute(command);
        //    return result;
        //}

        //[HttpPut("disable/{id}")]
        //public async Task<DepartmentDTO> DisablePermission(Guid id)
        //{
        //    var command = new DisablePermissionCommand(id);
        //    var result = await _commandDispatcher.Execute(command);
        //    return result;
        //}

        //[HttpPut("enable/{id}")]
        //public async Task<DepartmentDTO> EnablePermission(Guid id)
        //{
        //    var command = new EnableDepartmentCommand(id);
        //    var result = await _commandDispatcher.Execute(command);
        //    return result;
        //}

    }
}