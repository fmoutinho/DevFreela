using DevFreela.Application.Models;
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
        public string CreaditCardNumber { get; set; }
        public string Cvv {  get; set; }
        public string ExpiresAt { get; set; }
        public string FullName {  get; set; }
    }
}
