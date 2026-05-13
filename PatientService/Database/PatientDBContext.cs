using Microsoft.EntityFrameworkCore;
using PatientService.Entity;

namespace PatientService.Database
{
    public class PatientDBContext : DbContext
    {
        public PatientDBContext(DbContextOptions<PatientDBContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
    }
}
