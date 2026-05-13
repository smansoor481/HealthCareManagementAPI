using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.DTOs;
using UserService.Entity;
using UserService.Exceptions;
using UserService.Repository;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository repository, IMapper mapper, IConfiguration configuration,
            ILogger<UserService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            _logger.LogInformation("Register service started for Email: {Email}", dto.Email);

            var existingUser = await _repository.GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                _logger.LogWarning("Email already exists: {Email}", dto.Email);
                //return "Email already exists";
                throw new BadRequestException("Email already exists");
            }

            var user = _mapper.Map<User>(dto);

            await _repository.RegisterUserAsync(user);

            _logger.LogInformation("User saved successfully with Id: {UserId}", user.Id);

            return "User registered successfully";
        }


        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            _logger.LogInformation("Login service started for Email: {Email}", dto.Email);

            var user = await _repository.LoginAsync(dto.Email, dto.Password);

            if (user == null)
            {
                _logger.LogWarning("Invalid login attempt for Email: {Email}", dto.Email);
                //return null;
                throw new UnauthorizedException("Invalid email or password");
            }

            _logger.LogInformation("User validated. Generating token for UserId: {UserId}", user.Id);

            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            _logger.LogInformation("JWT token generated for UserId: {UserId}", user.Id);

            return new LoginResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}





//