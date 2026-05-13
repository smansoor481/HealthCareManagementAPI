using Microsoft.EntityFrameworkCore;
using PatientService.Database;
using PatientService.Entity;

namespace PatientService.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDBContext _context;
        private readonly ILogger<PatientRepository> _logger;

        public PatientRepository(PatientDBContext context, ILogger<PatientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Patient patient)
        {
            _logger.LogInformation("Saving patient profile to database. UserId: {UserId}", patient.UserId);

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Patient profile saved to database. PatientId: {PatientId}, UserId: {UserId}",
          patient.PatientId, patient.UserId);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all patients from database");
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetByUserIdAsync(int userId)
        {
            _logger.LogInformation("Fetching patient by UserId from database. UserId: {UserId}", userId);
            return await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Patient profile updated in database. PatientId: {PatientId}, UserId: {UserId}",
            patient.PatientId, patient.UserId);
        }
    }
}
