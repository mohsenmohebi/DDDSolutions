using DDDApi.Dtos;
using DDDApi.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DDDApi.Commands
{
    using DDDApi.Exceptions;
    using DDDApi.Repositories;
    using MediatR;
    using System;
    using System.Runtime.Serialization;

    // Command for resetting a user's password
    public class ResetPasswordCommand : IRequest
    {
        public ResetPasswordCommand(ResetPasswordDto dto)
        {
            Username = dto.Username;
            Email = dto.Email;
            NewPassword = new NewPassword(dto.NewPassword);
        }

        public string Username { get; }
        public string Email { get; }
        public NewPassword NewPassword { get; }
    }

    // Query for getting a user by username or email
    public class GetUserByUsernameOrEmailQuery : IRequest<User>
    {
        public GetUserByUsernameOrEmailQuery(string username, string email)
        {
            Username = username;
            Email = email;
        }

        public string Username { get; }
        public string Email { get; }
    }
      

    public class GetByIdQuery<T> : IRequest<T>
    {
        public GetByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    #region hanalders

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery<User>, User>
    {
        private readonly IUserRepository _userRepository;

        public GetByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetByIdQuery<User> request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            return user;
        }
    }

    // Handler for the ResetPasswordCommand
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetService _passwordResetService;

        public ResetPasswordCommandHandler(IUserRepository userRepository, IPasswordResetService passwordResetService)
        {
            _userRepository = userRepository;
            _passwordResetService = passwordResetService;
        }
             

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username) ??
                      await _userRepository.GetByEmailAsync(request.Email);


            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            await _passwordResetService.ResetPasswordAsync(user, request.NewPassword);

        }
    }


    // Handler for the GetUserByUsernameOrEmailQuery
    public class GetUserByUsernameOrEmailQueryHandler : IRequestHandler<GetUserByUsernameOrEmailQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByUsernameOrEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserByUsernameOrEmailQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(query.Username) ??
                       await _userRepository.GetByEmailAsync(query.Email);

            return user;
        }
    }

    #endregion

}
