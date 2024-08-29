using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _context;
        public ProjectRepository(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project.Id;
        }

        public async Task AddComent(ProjectComment projectComment)
        {
            await _context.ProjectComments.AddAsync(projectComment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Project>> GetAll()
        {
            return await _context.Projects
                                    .Include(P => P.Client)
                                    .Include(p => p.Freelancer)
                                    .Where(p => !p.IsDeleted)
                                    .ToListAsync();
        }

        public async Task<Project?> GetById(int id)
        {
            return await _context.Projects
                    .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Project?> GetDetailsById(int id)
        {
            return await _context.Projects
                    .Include(P => P.Client)
                    .Include(p => p.Freelancer)
                    .Include(p => p.Comments)
                    .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
