﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
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


        [HttpGet]
        public async Task<List<DepartmentProjection>> GetAllDepartments()
        {
            var query = new GetDepartmentsQuery();
            var result = await _queryProcessor.Process(query);
            return result;
        }

        [HttpPost("create")]
        public async Task<DepartmentProjection> CreateDepartment([FromBody] CreateDepartmentDTO input)
        {
            var command = new CreateDepartmentCommand(input);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }

        [HttpPut("remove")]
        public async Task<DepartmentProjection> RemoveDepartment(Guid id)
        {
            var command = new RemoveDepartmentCommand(id);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }

        [HttpPut("removeByName")]
        public async Task<DepartmentProjection> RemoveDepartmentByName([FromBody]RemoveDepartmentByNameInputDTO dto)
        {
            var command = new RemoveDepartmentByNameCommand(dto.Name);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }



    }
}