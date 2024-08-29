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
        public async Task<int> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task AddSkills(List<UserSkill> userSkills)
        {
            await _context.UserSkills.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users
                            .Include(u => u.Skills)
                            .ThenInclude(us => us.Skill)
                            .Where(u => !u.IsDeleted)
                            .ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetDetailsById(int id)
        {
            return await _context.Users
                .Include(u => u.Skills)
                .ThenInclude(us => us.Skill)
                .SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
