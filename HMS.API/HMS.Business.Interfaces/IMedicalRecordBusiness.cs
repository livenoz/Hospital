using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Patient;
using HMS.Common.Filters;
using System.Threading.Tasks;

namespace HMS.Business.Interfaces
{
    public interface IMedicalRecordBusiness
    {
        Task<MedicalRecordDto> Add(MedicalRecordDto model);
        Task<bool> Update(MedicalRecordDto model);
        Task<bool> Delete(int id);
        Task<IPaginatedList<MedicalRecordDto>> GetByPatientId(int patientId, int pageIndex, int pageSize);
        Task<IPaginatedList<MedicalRecordDto>> GetAll(int pageIndex, int pageSize);
        Task<IPaginatedList<MedicalRecordDto>> GetAll(MedicalRecordFilter filter);
        Task<IPaginatedList<MedicalRecordDto>> GetBeingTreated(int pageIndex, int pageSize);
        Task<MedicalRecordDto> GetById(int id);
        Task<bool> SetActive(int id, bool isActive);
    }
}
