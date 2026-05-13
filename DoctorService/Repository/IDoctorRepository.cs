using DoctorService.Entity;

namespace DoctorService.Repository
{
    public interface IDoctorRepository
    {
        Task AddAsync(Doctor doctor);
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task<Doctor?> GetByUserIdAsync(int userId);
        Task UpdateAsync(Doctor doctor);
    }
}
