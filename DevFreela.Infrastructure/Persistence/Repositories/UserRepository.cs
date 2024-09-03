using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _context;
        public UserRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task AddSkillsAsync(List<UserSkill> userSkills)
        {
            await _context.UserSkills.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                            .Include(u => u.Skills)
                            .ThenInclude(us => us.Skill)
                            .Where(u => !u.IsDeleted)
                            .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetDetailsByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Skills)
                .ThenInclude(us => us.Skill)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetDetailsByEmailAndPasswordAsync(string email, string passwordHash0)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(passwordHash0));
        }
    }
}
