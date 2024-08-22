using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;

        public UsersController(DevFreelaDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _dbContext.Users
                            .Include(u => u.Skills)
                            .ThenInclude(us => us.Skill)
                            .SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            var userViewModel = UserViewModel.FromEntity(user);

            return Ok(user);
        }
        [HttpPost]
        public IActionResult Post(CreateUserInputModel model)
        {
            var user = new User(model.FullName, model.Email, model.BirthDate);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
        {
            var userSkills = model.SkillIds.Select(s => new UserSkill(id, s)).ToList();

            _dbContext.UserSkills.AddRange(userSkills);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            //Processar a imagem

            return Ok(description);
        }
    }
}
