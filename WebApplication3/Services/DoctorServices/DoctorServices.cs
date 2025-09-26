using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Models.DTO;
using ApiAtencionesMédicas.Repositorys.DoctorRepository;

namespace ApiAtencionesMédicas.Services.DoctorServices
{
    public class DoctorServices : IDoctorServices
    {
        private readonly ILogger<DoctorServices> _logger;
        private readonly IDoctorRepository _IDoctorRepository;

        public DoctorServices(ILogger<DoctorServices> logger, IDoctorRepository IDoctorRepository)
        {
            _logger = logger;
            _IDoctorRepository = IDoctorRepository;
        }
        public async Task<List<DoctorDAO>> sp_ListDoctorsServices()
        {
            return await _IDoctorRepository.sp_ListDoctorsRepository();
        }
        public async Task<int?> sp_AddDoctorServices(DoctorDTO doctorDTO, string Doctor_CreatedBy)
        {
            return await _IDoctorRepository.sp_AddDoctorRepository(doctorDTO, Doctor_CreatedBy);
        }
        public async Task<int?> sp_UpdateDoctorServices(DoctorDTO doctorDTO, string Doctor_ModifiedBy, int Doctor_Id)
        {
            return await _IDoctorRepository.sp_UpdateDoctorRepository(doctorDTO, Doctor_ModifiedBy, Doctor_Id);
        }
        public async Task<int?> sp_DeleteDoctorServices(int Doctor_Id)
        {
            return await _IDoctorRepository.sp_DeleteDoctorRepository(Doctor_Id);
        }
    }
}
