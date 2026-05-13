using AppointmentService.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppointmentService.Database
{
    public class AppointmentDBContext : DbContext
    {
        public AppointmentDBContext(DbContextOptions<AppointmentDBContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
