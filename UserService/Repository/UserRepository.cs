using UserService.Data;
using UserService.Entity;
using Microsoft.EntityFrameworkCore;
namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserDBContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            _logger.LogInformation("Checking user by Email: {Email}", email);
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }


        public async Task<User> RegisterUserAsync(User user)
        {
            _logger.LogInformation("Saving user to database: {Email}", user.Email);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User saved with Id: {UserId}", user.Id);

            return user;
        }
        public async Task<User?> LoginAsync(string email, string password)
        {
            _logger.LogInformation("Validating login for Email: {Email}", email);

            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}
