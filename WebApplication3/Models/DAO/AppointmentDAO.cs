namespace ApiAtencionesMédicas.Models.DAO
{
    public class AppointmentDAO
    {
        public int Appointment_Id { get; set; }
        public string? PatientId { get; set; }
        public string? DoctorId { get; set; }
        public DateTime? Appointment_StartUtc { get; set; }
        public DateTime? Appointment_EndUtc { get; set; }
        public string Appointment_Diagnosis { get; set; }
        public string Appointment_Room { get; set; }
        public string Appointment_Status { get; set; }
        public string Appointment_CreatedBy { get; set; }
        public DateTime? Appointment_CreatedAt { get; set; }
        public string Appointment_ModifiedBy { get; set; }
        public DateTime? Appointment_ModifiedAt { get; set; }
    }
}
