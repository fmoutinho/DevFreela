using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public UpdateProjectCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);

            if (project is null)
            {
                return ResultViewModel.Failure("Project not found");
            }

            project.Update(request.Title, request.Description, request.TotalCost);

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
