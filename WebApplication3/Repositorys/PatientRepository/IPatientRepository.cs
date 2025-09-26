using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Models.DTO;

namespace ApiAtencionesMédicas.Repositorys.PatientRepository
{
    public interface IPatientRepository
    {
        Task<List<PatientDAO>> sp_ListPatientsRepository();
        Task<int?> sp_AddPatientRepository(PatientDTO patientDTO, string Patient_CreatedBy);
        Task<int?> sp_UpdatePatientRepository(PatientDTO patientDTO, string Patient_ModifiedBy, int Patient_Id);
        Task<int?> sp_DeletePatientRepository(int Patient_Id);
    }
}
