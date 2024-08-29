using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.UserQueries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<List<UserViewModel>>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultViewModel<List<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users =  await _userRepository.GetAll();

            var model = users.Select(UserViewModel.FromEntity).ToList();
            return ResultViewModel<List<UserViewModel>>.Success(model);
        }
    }
}
