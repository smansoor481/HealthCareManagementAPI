using DoctorService.Database;
using DoctorService.Entity;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DoctorDBContext _context;
        private readonly ILogger<DoctorRepository> _logger;

        public DoctorRepository(DoctorDBContext context, ILogger<DoctorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Doctor doctor)
        {
            _logger.LogInformation("Saving doctor to database");

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Doctor saved to database. DoctorId: {DoctorId}", doctor.DoctorId);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all doctors from database");

            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching doctor by id. DoctorId: {DoctorId}", id);

            return await _context.Doctors.FirstOrDefaultAsync(d => d.DoctorId == id);
        }

        public async Task<Doctor?> GetByUserIdAsync(int userId)
        {
            _logger.LogInformation("Fetching doctor by user id. UserId: {UserId}", userId);

            return await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _logger.LogInformation("Updating doctor in database. DoctorId: {DoctorId}", doctor.DoctorId);

            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Doctor updated in database. DoctorId: {DoctorId}", doctor.DoctorId);
        }
    }
}
