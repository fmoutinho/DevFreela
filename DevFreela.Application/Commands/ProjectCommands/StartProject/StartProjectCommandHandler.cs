using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.StartProject
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _projectRepository;
        public StartProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetDetailsById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Failure("Project not found");
            }

            project.Start();

            await _projectRepository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
