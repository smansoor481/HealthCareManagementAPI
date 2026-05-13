using DoctorService.DTOs;

namespace DoctorService.Services
{
    public interface IDoctorService
    {
        Task<string> AddDoctorAsync(CreateDoctorDto dto);
        Task<IEnumerable<DoctorResponseDto>> GetAllDoctorsAsync();
        Task<DoctorResponseDto> GetDoctorByUserIdAsync(int userId);
        Task<string> UpdateDoctorAsync(int id, UpdateDoctorDto dto);
    }
}
