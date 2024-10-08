﻿using DevFreela.Application.Models;
using DevFreela.Application.Notifications.ProjectCreated;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.InsertProject
{
    public class InsertProjectCommandHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMediator _mediator;
        public InsertProjectCommandHandler(IProjectRepository projectRepository, IMediator mediator)
        {
            _mediator = mediator;
            _projectRepository = projectRepository;
        }
        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _projectRepository.AddAsync(project);

            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await _mediator.Publish(projectCreated);

            return ResultViewModel<int>.Success(project.Id);
        }
    }
}
