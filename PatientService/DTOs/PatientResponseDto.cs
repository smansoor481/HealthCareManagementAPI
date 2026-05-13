namespace PatientService.DTOs
{
    public class PatientResponseDto
    {
        public int PatientId { get; set; }
        public string? PatientName { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? MedicalNotes { get; set; }
    }
}
