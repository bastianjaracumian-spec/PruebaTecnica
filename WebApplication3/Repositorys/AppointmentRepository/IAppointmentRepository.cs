using ApiAtencionesMédicas.Models.DAO;

namespace ApiAtencionesMédicas.Repositorys.AppointmentRepository
{
    public interface IAppointmentRepository
    {
        Task<List<AppointmentDAO>> sp_ListAppointmentRepository();
    }
}
