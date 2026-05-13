using UserService.Entity;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> RegisterUserAsync(User user);

        Task<User?> LoginAsync(string email, string password);
    }
}
