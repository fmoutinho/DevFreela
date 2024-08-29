using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.SkillQueries.GetSkillById
{
    public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, ResultViewModel<SkillViewModel>>
    {
        private readonly ISkillRepository _skillRepository;
        public GetSkillByIdQueryHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public async Task<ResultViewModel<SkillViewModel>> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            var skill = await _skillRepository.GetById(request.Id);

            if(skill is null)
            {
                return ResultViewModel<SkillViewModel>.Failure("Skill not found");
            }

            var model = SkillViewModel.FromEntity(skill);
            return ResultViewModel<SkillViewModel>.Success(model);
        }
    }
}
