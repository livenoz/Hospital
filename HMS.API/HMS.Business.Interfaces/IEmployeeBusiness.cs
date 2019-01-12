using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Employee;
using HMS.Common.Dtos.Patient;
using System.Threading.Tasks;

namespace HMS.Business.Interfaces
{
    public interface IEmployeeBusiness
    {
        Task<IPaginatedList<DoctorDto>> GetDoctors(int pageIndex, int pageSize);
        Task<IPaginatedList<NurseDto>> GetNurses(int pageIndex, int pageSize);
    }
}
