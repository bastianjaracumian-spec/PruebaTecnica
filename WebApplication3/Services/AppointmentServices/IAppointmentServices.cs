using ApiAtencionesMédicas.Models.DAO;

namespace ApiAtencionesMédicas.Services.AppointmentServices
{
    public interface IAppointmentServices
    {
        Task<List<AppointmentDAO>> sp_ListAppointmentServices();
    }
}
