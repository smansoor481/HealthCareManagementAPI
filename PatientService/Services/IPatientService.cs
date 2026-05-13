using PatientService.DTOs;

namespace PatientService.Services
{
    public interface IPatientService
    {
        Task<string> CreateProfileAsync(CreatePatientDto dto, int userId);
        Task<PatientResponseDto> GetMyProfileAsync(int userId);

        //admin can access all
        Task<IEnumerable<PatientResponseDto>> GetAllPatientsAsync();
        Task<string> UpdateProfileAsync(UpdatePatientDto dto, int userId);
    }
}
