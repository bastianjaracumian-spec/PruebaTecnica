namespace ApiAtencionesMédicas.Models.DAO
{
    public class JWTUserDAO
    {
        public int User_Id { get; set; }
        public string? User_FirtName { get; set; }
        public string? User_LastName { get; set; }
        public string? User_Email { get; set; }
        public string? User_Phone { get; set; }
        public string? User_RUT { get; set; }
        public bool User_Active { get; set; }
    }
}
