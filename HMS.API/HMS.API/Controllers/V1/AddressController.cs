using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HMS.API.Attributes;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Address;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class AddressController : ControllerBase
    {
        // GET: api/Address
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Address/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Address
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Address/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public async Task<IPaginatedList<CountryDto>> GetCountries(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<IPaginatedList<ProvinceDto>> GetProvinces(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<IPaginatedList<DistrictDto>> GetDistricts(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

    }
}
