using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Models.DTO;
using ApiAtencionesMédicas.Repositorys.JWTRepository.JWTRepository;
using ApiAtencionesMédicas.Repositorys.PatientRepository;

namespace ApiAtencionesMédicas.Services.PatientServices
{
    public class PatientServices : IPatientServices
    {
        private readonly ILogger<PatientServices> _logger;
        private readonly IPatientRepository _IPatientRepository;

        public PatientServices(ILogger<PatientServices> logger, IPatientRepository IPatientRepository)
        {
            _logger = logger;
            _IPatientRepository = IPatientRepository;
        }
        public async Task<List<PatientDAO>> sp_GetUserServices()
        {
            return await _IPatientRepository.sp_ListPatientsRepository();
        }
        public async Task<PatientDAO> sp_GetUserServicesByRutServices(string Patient_RUT)
        {
            var ListPatients =  await _IPatientRepository.sp_ListPatientsRepository();

            return ListPatients.Find(x => x.Patient_RUT == Patient_RUT);
        }
        public async Task<int?> sp_AddPatientsServices(PatientDTO patientDTO, string Patient_CreatedBy)
        {
            return await _IPatientRepository.sp_AddPatientRepository(patientDTO,  Patient_CreatedBy);
        }
        public async Task<int?> sp_UpdatePatientsServices(PatientDTO patientDTO, string Patient_ModifiedBy, int Patient_Id)
        {
            return await _IPatientRepository.sp_UpdatePatientRepository(patientDTO, Patient_ModifiedBy, Patient_Id);
        }
        public async Task<int?> sp_DeletePatientServices(int Patient_Id)
        {
            return await _IPatientRepository.sp_DeletePatientRepository(Patient_Id);
        }
    }
}
