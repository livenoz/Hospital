using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Patient;
using HMS.Common.Filters;
using HMS.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMS.Business.Interfaces
{
    public interface IPatientBusiness
    {
        Task<PatientDto> Add(PatientDto model);
        Task<bool> Update(PatientDto model);
        Task<bool> Delete(int id);
        Task<bool> SetActive(int id, bool isActive);
        Task<IPaginatedList<PatientDto>> GetAll(int pageIndex, int pageSize);
        Task<IPaginatedList<PatientDto>> GetAll(PatientFilter filter);
        Task<PatientDto> GetById(int id);
    }
}
