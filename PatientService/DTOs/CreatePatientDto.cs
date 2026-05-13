using System.ComponentModel.DataAnnotations;

namespace PatientService.DTOs
{
    public class    CreatePatientDto
    {
        [Required(ErrorMessage = "Patient name is required")]
        [MaxLength(100, ErrorMessage = "Patient name cannot exceed 100 characters")]
        public string? PatientName { get; set; }

        [Range(0, 120, ErrorMessage = "Age must be between 0 and 120")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [MaxLength(10, ErrorMessage = "Gender cannot exceed 10 characters")]
        public string? Gender { get; set; }

        //[Required(ErrorMessage = "Phone number is required")]
        //[Phone(ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [MaxLength(500, ErrorMessage = "Medical notes cannot exceed 500 characters")]
        public string? MedicalNotes { get; set; }
    }
}
