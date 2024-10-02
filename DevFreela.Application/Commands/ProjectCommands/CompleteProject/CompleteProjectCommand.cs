using DevFreela.Application.Models;
using DevFreela.Core.DTOs;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.CompleteProject
{
    public class CompleteProjectCommand : IRequest<ResultViewModel>
    {
        public CompleteProjectCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public PaymentInfoDTO paymentInfo { get; set; }
    }
}
