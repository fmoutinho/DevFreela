using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.InsertComment
{
    public class InsertComentCommandHandler : IRequestHandler<InsertComentCommand, ResultViewModel>
    {
        private readonly IProjectRepository _projectRepository;
        public InsertComentCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<ResultViewModel> Handle(InsertComentCommand request, CancellationToken cancellationToken)
        {
            var projectExists = await _projectRepository.Exists(request.IdProject);

            if (!projectExists)
            {
                return ResultViewModel.Failure("Project not found");
            }

            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _projectRepository.AddComent(comment);

            return ResultViewModel.Success();
        }
    }
}
