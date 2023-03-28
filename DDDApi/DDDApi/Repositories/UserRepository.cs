using DDDApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDDApi.Repositories
{
    // Repository implementation using Entity Framework Core
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Set<User>().FindAsync(id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }

    // Domain service implementation for resetting passwords
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IUserRepository _userRepository;

        public PasswordResetService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ResetPasswordAsync(User user, NewPassword newPassword)
        {
            // Update the user's password hash
            user.PasswordHash = HashPassword(newPassword.Value);

            // Save the updated user
            await _userRepository.UpdateAsync(user);
        }

        private string HashPassword(string password)
        {
            // Use a secure password hashing algorithm here
            return password;
        }
    }
}
