using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Commands.ProjectCommands.InsertProject;
using DevFreela.Application.Commands.ProjectCommands.InsertComment;
using DevFreela.Application.Commands.ProjectCommands.UpdateProject;
using DevFreela.Application.Commands.ProjectCommands.CompleteProject;
using DevFreela.Application.Commands.ProjectCommands.StartProject;
using DevFreela.Application.Commands.ProjectCommands.DeleteProject;
using DevFreela.Application.Queries.ProjectQueries.GetAllProjects;
using DevFreela.Application.Queries.ProjectQueries.GetProjectById;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjectsController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllProjectsQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post(InsertProjectCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpPut("{id}/Start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Start(int id)
        {
            var result = await _mediator.Send(new StartProjectCommand(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpPut("{id}/Complete")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Complete([FromRoute]int id, [FromBody] CompleteProjectCommand completeProjectCommand)
        {
            completeProjectCommand.ProjectId = id;

            var result = await _mediator.Send(completeProjectCommand);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Accepted();
        }

        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, InsertComentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }
    }
}
