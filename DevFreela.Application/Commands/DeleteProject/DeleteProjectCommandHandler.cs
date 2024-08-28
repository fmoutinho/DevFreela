using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public DeleteProjectCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }
        public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project =  await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project is null)
            {
                return ResultViewModel.Failure("Project not found");
            }
            project.Delete();

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
