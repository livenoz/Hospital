using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Address;
using HMS.Common.Dtos.User;
using HMS.Common.Responses.User;
using HMS.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMS.Business.Interfaces
{
    public interface IAddressBusiness
    {
        Task<IPaginatedList<CountryDto>> GetCountries(int pageIndex, int pageSize);
        Task<IPaginatedList<ProvinceDto>> GetProvinces(int pageIndex, int pageSize);
        Task<IPaginatedList<DistrictDto>> GetDistricts(int pageIndex, int pageSize);
    }
}
