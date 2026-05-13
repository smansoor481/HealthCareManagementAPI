using System.ComponentModel.DataAnnotations;

namespace PatientService.Entity
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? PatientName { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        [Required]
        [MaxLength(10)]
        public string? Gender { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(500)]
        public string? MedicalNotes { get; set; }
    }
}