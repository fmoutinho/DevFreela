using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _context;
        public UserService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var user = _context.Users
                .Include(u => u.Skills)
                .ThenInclude(us => us.Skill)
                .SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return ResultViewModel<UserViewModel>.Failure("User not found");
            }

            var model = UserViewModel.FromEntity(user);
            return ResultViewModel<UserViewModel>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateUserInputModel model)
        {
            var user = new User(model.FullName, model.Email, model.BirthDate);

            _context.Users.Add(user);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(user.Id);
        }

        public void InsertUserSkills(UserSkillsInputModel model)
        {
            var userSkills = model.SkillIds.Select(s => new UserSkill(model.Id, s)).ToList();

            _context.UserSkills.AddRange(userSkills);
            _context.SaveChanges();
        }

        public ResultViewModel<List<UserViewModel>> GetAll(string search)
        {
            var users = _context.Users
                .Include(u => u.Skills)
                .ThenInclude(us => us.Skill)
                .Where(u => !u.IsDeleted && (search == "" || u.FullName.Contains(search)))
                .ToList();

            var model = users.Select(UserViewModel.FromEntity).ToList();
            return ResultViewModel<List<UserViewModel>>.Success(model);
        }
    }
}
