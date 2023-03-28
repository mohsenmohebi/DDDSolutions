using DDDApi.Commands;
using DDDApi.Dtos;
using DDDApi.Entities;
using DDDApi.Exceptions;
using DDDApi.Models;
using MediatR;

namespace DDDApi.Services
{
    // Service interface for resetting user passwords
    public interface IPasswordResetService
    {
        Task<UserViewModel> ResetPasswordAsync(ResetPasswordViewModel viewModel);
    }

    // Service implementation for resetting user passwords
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IMediator _mediator;

        public PasswordResetService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserViewModel> ResetPasswordAsync(ResetPasswordViewModel viewModel)
        {
            var user = await _mediator.Send(new GetUserByUsernameOrEmailQuery(viewModel.Username, viewModel.Email));
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var command = new ResetPasswordDto
            {
                Username = user.Username,
                Email = user.Email,
                NewPassword = viewModel.NewPassword
            };

            await _mediator.Send(command);

            return new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
    }

}
