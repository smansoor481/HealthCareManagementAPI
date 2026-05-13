using System.ComponentModel.DataAnnotations;

namespace DoctorService.DTOs
{
    public class UpdateDoctorDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid doctor login UserId")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Doctor name is required")]
        [MaxLength(50, ErrorMessage = "Doctor name cannot exceed 50 characters")]
        public string? DoctorName { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        [MaxLength(50, ErrorMessage = "Specialization cannot exceed 50 characters")]
        public string? Specialization { get; set; }
    }
}
