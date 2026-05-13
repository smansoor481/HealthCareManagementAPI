using PatientService.Entity;

namespace PatientService.Repository
{
    public interface IPatientRepository
    {
        Task AddAsync(Patient patient);
        Task<Patient?> GetByUserIdAsync(int userId);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task UpdateAsync(Patient patient);

    }
}
