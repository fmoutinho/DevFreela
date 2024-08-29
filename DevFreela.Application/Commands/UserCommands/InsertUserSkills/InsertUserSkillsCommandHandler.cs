using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.InsertUserSkills
{
    public class InsertUserSkillsCommandHandler : IRequestHandler<InsertUserSkillsCommand, ResultViewModel>
    {
        private readonly IUserRepository _userRepository;
        public InsertUserSkillsCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultViewModel> Handle(InsertUserSkillsCommand request, CancellationToken cancellationToken)
        {
            var userSkills = request.SkillIds.Select(s => new UserSkill(request.Id, s)).ToList();

           await  _userRepository.AddSkills(userSkills);

            return ResultViewModel.Success();
        }
    }
}
