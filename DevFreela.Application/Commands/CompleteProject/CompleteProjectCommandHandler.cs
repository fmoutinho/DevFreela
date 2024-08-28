using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.CompleteProject
{
    public class CompleteProjectCommandHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public CompleteProjectCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project is null)
            {
                return ResultViewModel.Failure("Project not found");
            }
            project.Complete();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
