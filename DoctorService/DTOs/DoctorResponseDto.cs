namespace DoctorService.DTOs
{
    public class DoctorResponseDto
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public string? DoctorName { get; set; }
        public string? Specialization { get; set; }
    }
}
