using DoctorService.Entity;
using Microsoft.EntityFrameworkCore;

namespace DoctorService.Database
{
    public class DoctorDBContext : DbContext
    {
        public DoctorDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
    }
}
