using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ProjectCommands.InsertProject
{
    public class InsertProjectCommandValidator : AbstractValidator<InsertProjectCommand>
    {
        public InsertProjectCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                    .WithMessage("Cannot be empty")
                .MaximumLength(50)
                    .WithMessage("Maximum size is 50");

            RuleFor(p => p.TotalCost)
                .GreaterThanOrEqualTo(1000)
                    .WithMessage("Project must cost at least 1000");
        }
    }
}
