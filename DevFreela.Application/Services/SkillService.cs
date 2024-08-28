using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _context;
        public SkillService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<List<Skill>> GetAll(string search)
        {
            var skills = _context.Skills
                .Include(s => s.UserSkills)
                .ThenInclude(us => us.User)
                .Where(s => !s.IsDeleted && (search == "" || s.Description.Contains(search)))
                .ToList();

            return ResultViewModel<List<Skill>>.Success(skills);
        }

        public ResultViewModel<Skill> GetById(int id)
        {
            var skill = _context.Skills
                .Include(s => s.UserSkills)
                .ThenInclude(us => us.User)
                .SingleOrDefault(s => s.Id == id);

            return ResultViewModel<Skill>.Success(skill);
        }

        public ResultViewModel<int> Insert(CreateSkillInputModel model)
        {
            var skill = new Skill(model.Description);

            _context.Skills.Add(skill);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
