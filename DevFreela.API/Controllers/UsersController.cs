using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Commands.UserCommands.InsertUser;
using DevFreela.Application.Queries.UserQueries.GetAllUsers;
using DevFreela.Application.Queries.UserQueries.GetUserById;
using DevFreela.Application.Commands.UserCommands.InsertUserSkills;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post(InsertUserCommand model)
        {
            var result = _mediator.Send(model);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, model);
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(InsertUserSkillsCommand model)
        {
            var result = await _mediator.Send(model);

            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public async Task<IActionResult> PostProfilePicture(IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            //Processar a imagem

            return Ok(description);
        }
    }
}
