using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.SkillComands.InsertSkill
{
    public class InsertSkillCommandHandler : IRequestHandler<InsertSkillCommand, ResultViewModel<int>>
    {
        private readonly ISkillRepository _skillRepository;

        public InsertSkillCommandHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<ResultViewModel<int>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);

            await _skillRepository.Add(skill);

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
