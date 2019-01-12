using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Patient;
using System.Threading.Tasks;

namespace HMS.Business.Interfaces
{
    public interface ITreatmentBusiness
    {
        Task<TreatmentDetailDto> Add(TreatmentDetailDto model);
        Task<bool> Update(TreatmentDetailDto model);
        Task<bool> Delete(int id);
        Task<IPaginatedList<TreatmentDto>> GetAll(int pageIndex, int pageSize);
        Task<IPaginatedList<TreatmentDto>> GetByMedicalRecordId(int medicalRecordId, int pageIndex, int pageSize);
        Task<IPaginatedList<TreatmentDto>> GetByPatientId(int patientId, int pageIndex, int pageSize);
        Task<TreatmentDto> GetById(int id);
    }
}
