using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.UserCommands.InsertUser
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {
        private readonly IUserRepository _userRepository;
        public InsertUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.FullName, request.Email, request.BirthDate);

            await _userRepository.Add(user);

            return ResultViewModel<int>.Success(user.Id);
        }
    }
}
