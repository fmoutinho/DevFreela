using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetDetailsByIdAsync(int id);
        Task<Project?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task AddComentAsync(ProjectComment projectComment);

    }
}
