using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultViewModel<LoginUserViewModel>>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel<LoginUserViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = await _userRepository.GetDetailsByEmailAndPasswordAsync(request.Email, passwordHash);

            if (user is null)
            {
                return ResultViewModel<LoginUserViewModel>.Failure("User not found");
            }

            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            var loginUserViewModel = new LoginUserViewModel(user.Email, token);

            return ResultViewModel<LoginUserViewModel>.Success(loginUserViewModel);
        }
    }
}
