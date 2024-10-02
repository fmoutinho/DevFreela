using DevFreela.Application.Models;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.CompleteProject
{
    public class CompleteProjectCommandHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentService _paymentService;
        public CompleteProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService)
        {
            _projectRepository = projectRepository;
            _paymentService = paymentService;
        }
        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetDetailsByIdAsync(request.Id);

            if (project is null)
            {
                return ResultViewModel.Failure("Project not found");
            }

            _paymentService.ProcessPayment(new PaymentInfoDTO(request.Id, request.FullName, request.CreditCardNumber, request.Cvv, request.ExpiresAt));

            project.SetPaymentPending();

            await _projectRepository.UpdateAsync(project);

            return ResultViewModel.Success();
        }
    }
}
