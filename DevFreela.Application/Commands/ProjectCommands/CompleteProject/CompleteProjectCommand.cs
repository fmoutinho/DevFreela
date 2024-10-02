using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.CompleteProject
{
    public class CompleteProjectCommand : IRequest<ResultViewModel>
    {
        public CompleteProjectCommand(int projectId)
        {
            ProjectId = projectId;
        }
        public int ProjectId { get; set; }
        public string CreditCardNumber { get; set; }
        public string Cvv {  get; set; }
        public string ExpiresAt { get; set; }
        public string FullName {  get; set; }
    }
}
