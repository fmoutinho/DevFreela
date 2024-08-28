using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _context;
        public GetProjectByIdQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                    .Include(P => P.Client)
                    .Include(p => p.Freelancer)
                    .Include(p => p.Comments)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);

            if (project is null)
            {
                return ResultViewModel<ProjectViewModel>.Failure("Project not found");
            }

            var model = ProjectViewModel.FromEntity(project);
            return ResultViewModel<ProjectViewModel>.Success(model);
        }
    }
}
