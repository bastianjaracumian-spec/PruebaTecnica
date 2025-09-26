using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Models.DTO;

namespace ApiAtencionesMédicas.Services.PatientServices
{
    public interface IPatientServices
    {
        Task<List<PatientDAO>> sp_GetUserServices();
        Task<PatientDAO> sp_GetUserServicesByRutServices(string Patient_RUT);
        Task<int?> sp_AddPatientsServices(PatientDTO patientDTO, string Patient_CreatedBy);
        Task<int?> sp_UpdatePatientsServices(PatientDTO patientDTO, string Patient_ModifiedBy, int Patient_Id);
        Task<int?> sp_DeletePatientServices(int Patient_Id);

    }
}
