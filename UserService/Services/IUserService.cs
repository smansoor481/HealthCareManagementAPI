using UserService.DTOs;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDto dto);

        Task<LoginResponseDto?> LoginAsync(LoginDto dto);
    }
}
