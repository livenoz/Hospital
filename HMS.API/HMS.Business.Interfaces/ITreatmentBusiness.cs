using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Patient;
using System.Threading.Tasks;

namespace HMS.Business.Interfaces
{
    public interface ITreatmentBusiness
    {
        Task<TreatmentDiseaseDto> Add(TreatmentDiseaseDto model);
        Task<bool> Update(TreatmentDiseaseDto model);
        Task<bool> Delete(int id);
        Task<IPaginatedList<TreatmentDto>> GetAll(int pageIndex, int pageSize);
        Task<IPaginatedList<TreatmentDto>> GetByMedicalRecordId(int medicalRecordId, int pageIndex, int pageSize);
        Task<IPaginatedList<TreatmentDto>> GetByPatientId(int patientId, int pageIndex, int pageSize);
        Task<TreatmentDiseaseDto> GetById(int id);
    }
}
