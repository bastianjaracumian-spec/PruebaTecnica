using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Repositorys.AppointmentRepository;
using ApiAtencionesMédicas.Repositorys.DoctorRepository;

namespace ApiAtencionesMédicas.Services.AppointmentServices
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly ILogger<AppointmentServices> _logger;
        private readonly IAppointmentRepository _IAppointmentRepository;

        public AppointmentServices(ILogger<AppointmentServices> logger, IAppointmentRepository IAppointmentRepository)
        {
            _logger = logger;
            _IAppointmentRepository = IAppointmentRepository;
        }
        public async Task<List<AppointmentDAO>> sp_ListAppointmentServices()
        {
            return await _IAppointmentRepository.sp_ListAppointmentRepository();
        }
    }
}
