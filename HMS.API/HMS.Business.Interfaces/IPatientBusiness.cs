using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos;
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
        Task<IPaginatedList<PatientDto>> GetAll(int pageIndex, int pageSize);
        Task<PatientDto> GetById(int id);
    }
}
