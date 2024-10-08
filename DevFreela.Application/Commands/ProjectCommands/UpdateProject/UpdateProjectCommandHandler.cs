﻿using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _projectRepository;
        public UpdateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetDetailsByIdAsync(request.IdProject);

            if (project is null)
            {
                return ResultViewModel.Failure("Project not found");
            }

            project.Update(request.Title, request.Description, request.TotalCost);

            await _projectRepository.UpdateAsync(project);

            return ResultViewModel.Success();
        }
    }
}
