using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.API.Attributes;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientBusiness _patientBusiness;

        public PatientController(IPatientBusiness patientBusiness)
        {
            _patientBusiness = patientBusiness;
        }

        // GET: api/Patient
        [HttpGet]
        public async Task<IPaginatedList<PatientDto>> Get(int pageIndex, int pageSize)
        {
            return await _patientBusiness.GetAll(pageIndex, pageSize);
        }

        // GET: api/Patient/5
        [BearerAuthorize]
        [HttpGet("{id}")]
        public async Task<PatientDto> Get(int id)
        {
            return await _patientBusiness.GetById(id);
        }

        // POST: api/Patient
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Patient/5
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
