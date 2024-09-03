using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetDetailsByIdAsync(int id);
        Task<int> AddAsync(User user);
        Task AddSkillsAsync(List<UserSkill> userSkills);
        Task<bool> ExistsAsync(int id);
        Task<User?> GetDetailsByEmailAndPasswordAsync(string email, string passwordHash0);
    }
}
