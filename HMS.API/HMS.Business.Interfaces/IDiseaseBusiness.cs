using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Patient;
using System.Threading.Tasks;

namespace HMS.Business.Interfaces
{
    public interface IDiseaseBusiness
    {
        Task<IPaginatedList<DiseaseDto>> GetAll(int pageIndex, int pageSize);
        

    }
}
