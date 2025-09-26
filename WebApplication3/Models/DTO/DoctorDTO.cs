namespace ApiAtencionesMédicas.Models.DTO
{
    public class DoctorDTO
    {
        public string Doctor_FirstName { get; set; }
        public string Doctor_LastName { get; set; }
        public string Doctor_Email { get; set; }
        public string Doctor_Phone { get; set; }
        public string Doctor_LicenseNumber { get; set; }
        public int SpecialityId { get; set; }
    }
}
