using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.InsertProject
{
    public class InsertProjectCommand : IRequest<ResultViewModel<int>>
    {
        public InsertProjectCommand(string title, string description, int idClient, int idFreelancer, decimal totalCost)
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            TotalCost = totalCost;
        }

        public string Title { get; }
        public string Description { get; }
        public int IdClient { get; }
        public int IdFreelancer { get; }
        public decimal TotalCost { get; }

        public Project ToEntity()
            => new(Title, Description, IdClient, IdFreelancer, TotalCost);
    }
}