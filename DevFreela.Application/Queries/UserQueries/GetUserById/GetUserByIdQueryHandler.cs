using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetDetailsByIdAsync(request.Id);

            if (user is null)
            {
                return ResultViewModel<UserViewModel>.Failure("User not found");
            }

            var model = UserViewModel.FromEntity(user);
            return ResultViewModel<UserViewModel>.Success(model);
        }
    }
}
