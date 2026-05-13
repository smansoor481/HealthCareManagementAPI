using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.DTOs;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<AuthController> _logger;
        private readonly UserDBContext _context;

        public AuthController(IUserService service, ILogger<AuthController> logger, UserDBContext context)
        {
            _service = service;
            _logger = logger;
            _context = context;
        }

        //public AuthController(IUserService service, ILogger<AuthController> logger)
        //{
        //    _service = service;
        //    _logger = logger;
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            _logger.LogInformation("Register API called for Email: {Email}", dto.Email);

            var result = await _service.RegisterAsync(dto);

            _logger.LogInformation("User registered successfully for Email: {Email}", dto.Email);

            return Ok(new
            {
                success = true,
                message = result
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            _logger.LogInformation("Login API called for Email: {Email}", dto.Email);

            var result = await _service.LoginAsync(dto);

            _logger.LogInformation("Login successful for Email: {Email}", dto.Email);

            return Ok(new
            {
                success = true,
                message = "Login successful",
                data = result
            });
        }




        [HttpGet("doctor-users")]
        public async Task<IActionResult> GetDoctorUsers()
        {
            var doctors = await _context.Users
                .Where(u => u.Role == "Doctor")
                .Select(u => new
                {
                    id = u.Id,
                    username = u.Username,
                    email = u.Email
                }).ToListAsync();
                

            return Ok(doctors);
        }



        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new
            {
                success = true,
                message = "You are authenticated!"
            });
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("patient-only")]
        public IActionResult PatientOnly()
        {
            return Ok(new
            {
                success = true,
                message = "Only patient can access"
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok(new
            {
                success = true,
                message = "Only admin can access"
            });
        }
    }
}