using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.SkillQueries.GetAllSkills
{
    public class GetAllSkillQueryHandler : IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillViewModel>>>
    {
        private readonly ISkillRepository _skillRepository;

        public GetAllSkillQueryHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await _skillRepository.GetAll();
            var models = skills.Select(SkillViewModel.FromEntity).ToList();
            return ResultViewModel<List<SkillViewModel>>.Success(models);
        }
    }
}
