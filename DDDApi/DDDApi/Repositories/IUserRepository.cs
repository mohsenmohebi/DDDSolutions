using DDDApi.Entities;

namespace DDDApi.Repositories
{
    // Repository interface for managing users
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task UpdateAsync(User user);
    }

    // Domain service interface for resetting user passwords
    public interface IPasswordResetService
    {
        Task ResetPasswordAsync(User user, NewPassword newPassword);
    }

}
