using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.InsertUser
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public InsertUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }
        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = new User(request.FullName, request.Email, request.BirthDate, passwordHash, request.Role);

            await _userRepository.AddAsync(user);

            return ResultViewModel<int>.Success(user.Id);
        }
    }
}
