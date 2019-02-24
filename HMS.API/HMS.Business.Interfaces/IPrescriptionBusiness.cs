using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Patient;
using HMS.Common.Filters;
using HMS.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMS.Business.Interfaces
{
    public interface IPrescriptionBusiness
    {
        Task<List<int>> Add(List<PrescriptionDetailDto> models);
        Task<bool> Update(int treatmentDetailId, List<PrescriptionDetailDto> models);
        Task<bool> Delete(int id);
        Task<IPaginatedList<PrescriptionDetailDto>> GetByTreatmentDetailId(int treatmentDetailId);
        Task<IPaginatedList<UnitDto>> GetUnits(int pageIndex, int pageSize);
        Task<IPaginatedList<DrugDto>> GetDrugs(int pageIndex, int pageSize);
    }
}
