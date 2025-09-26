using ApiAtencionesMédicas.Models.DAO;
using ApiAtencionesMédicas.Models.DTO;

namespace ApiAtencionesMédicas.Services.DoctorServices
{
    public interface IDoctorServices
    {
        Task<List<DoctorDAO>> sp_ListDoctorsServices();
        Task<int?> sp_AddDoctorServices(DoctorDTO doctorDTO, string Doctor_CreatedBy);
        Task<int?> sp_UpdateDoctorServices(DoctorDTO doctorDTO, string Doctor_ModifiedBy, int Doctor_Id);
        Task<int?> sp_DeleteDoctorServices(int Doctor_Id);
    }
}
