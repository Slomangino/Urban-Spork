using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrbanSpork.Common.DataTransferObjects.Position;
using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.API.Controllers
{
    [Route("api/[controller]")]
    public class PositionController : Controller
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandDispatcher _commandDispatcher;

        public PositionController(IQueryProcessor queryProcessor, ICommandDispatcher commandDispatcher)
        {
            _queryProcessor = queryProcessor;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet("getAll")]
        public async Task<List<PositionProjection>> GetAllPositions()
        {
            var query = new GetAllPositionsQuery();
            var result = await _queryProcessor.Process(query);
            return result;
        }

        [HttpGet("getByDepartment")]
        public async Task<List<PositionProjection>> GetPositionsByDepartment(string name)
        {
            var query = new GetPositionsByDepartmentNameQuery(name);
            var result = await _queryProcessor.Process(query);
            return result;
        }

        [HttpPost("create")]
        public async Task<PositionProjection> CreatePosition([FromBody] CreatePositionDTO input)
        {
            var command = new CreatePositionCommand(input);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }

        [HttpPut("remove")]
        public async Task<PositionProjection> RemovePosition(Guid id)
        {
            var command = new RemovePositionCommand(id);
            var result = await _commandDispatcher.Execute(command);
            return result;
        }
    }
}