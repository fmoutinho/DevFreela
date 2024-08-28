using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.CompleteProject
{
    public class CompleteProjectCommandHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _projectRepository;
        public CompleteProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetDetailsById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Failure("Project not found");
            }

            project.Complete();

            await _projectRepository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
