using System.ComponentModel.DataAnnotations;

namespace AppointmentService.DTOs
{
    public class BookAppointmentDto
    {
        [Required(ErrorMessage = "DoctorId is required")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Appointment date is required")]
        public DateTime AppointmentDate { get; set; }
    }
}
