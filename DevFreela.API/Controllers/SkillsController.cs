using DevFreela.API.Entities;
using DevFreela.API.Models;
using DevFreela.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;
        public SkillsController(DevFreelaDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var skills = _dbContext.Skills.ToList();

            return Ok(skills);
        }

        [HttpPost]
        public IActionResult Post(CreateSkillInputModel model)
        { 
            var skill = new Skill(model.Description);
            return Ok(); 
        }
    }
}
