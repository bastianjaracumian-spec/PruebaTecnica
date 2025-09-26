using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Models.DTO;

namespace ApiAtencionesMédicas.Repositorys.DoctorRepository
{
    public interface IDoctorRepository
    {
        Task<List<DoctorDAO>> sp_ListDoctorsRepository();
        Task<int?> sp_AddDoctorRepository(DoctorDTO doctorDTO, string Doctor_CreatedBy);
        Task<int?> sp_UpdateDoctorRepository(DoctorDTO doctorDTO, string Doctor_ModifiedBy, int Doctor_Id);
        Task<int?> sp_DeleteDoctorRepository(int Doctor_Id);
    }
}
