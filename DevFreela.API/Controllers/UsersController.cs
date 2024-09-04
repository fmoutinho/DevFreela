using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Commands.UserCommands.InsertUser;
using DevFreela.Application.Queries.UserQueries.GetAllUsers;
using DevFreela.Application.Queries.UserQueries.GetUserById;
using DevFreela.Application.Commands.UserCommands.InsertUserSkills;
using DevFreela.Application.Models;
using DevFreela.Application.Commands.UserCommands.LoginUser;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
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
        [AllowAnonymous]
        public async Task<IActionResult> Post(InsertUserCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, command);
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(InsertUserSkillsCommand command)
        {
            var result = await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public async Task<IActionResult> PostProfilePicture(IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            //Processar a imagem

            return Ok(description);
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }
    }
}
