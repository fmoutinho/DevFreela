using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly DevFreelaDbContext _context;
        public GetAllProjectsQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
                .Include(P => P.Client)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();
            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }
    }
}
