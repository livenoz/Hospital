using System;
using System.Threading.Tasks;
using HMS.API.Attributes;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Employee;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        // GET: api/Disease
        [HttpGet]
        public async Task<IPaginatedList<DiseaseDto>> Get(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        // GET: api/Disease/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Disease
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Disease/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
