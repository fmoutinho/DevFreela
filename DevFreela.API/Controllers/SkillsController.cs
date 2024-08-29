using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.Queries.SkillQueries.GetAllSkills;
using DevFreela.Application.Commands.SkillComands.InsertSkill;
using DevFreela.Application.Queries.SkillQueries.GetSkillById;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skills = _mediator.Send(new GetAllSkillsQuery());

            return Ok(skills);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InsertSkillCommand command)
        {
            var result = _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetSkillByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
