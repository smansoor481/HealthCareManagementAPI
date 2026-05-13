namespace DoctorService.Entity
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        // This stores the UserService Id of the doctor login account.
        // It fixes the common issue where logged-in doctor UserId != DoctorId.
        public int UserId { get; set; }

        public string? DoctorName { get; set; }
        public string? Specialization { get; set; }
    }
}
