using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Patient;
using System.Threading.Tasks;

namespace HMS.Business.Interfaces
{
    public interface ITreatmentDetailBusiness
    {
        Task<TreatmentDetailDto> Add(TreatmentDetailDto model);
        Task<bool> Update(TreatmentDetailDto model);
        Task<bool> Delete(int id);
        Task<IPaginatedList<TreatmentDetailDto>> GetAll(int pageIndex, int pageSize);
        Task<IPaginatedList<TreatmentDetailDto>> GetByMedicalRecordId(int medicalRecordId, int pageIndex, int pageSize);
        Task<IPaginatedList<TreatmentDetailDto>> GetByPatientId(int patientId, int pageIndex, int pageSize);
        Task<IPaginatedList<TreatmentDetailDto>> GetByTreatmentId(int treatmentId, int pageIndex, int pageSize);
        Task<TreatmentDetailDto> GetById(int id);
    }
}
