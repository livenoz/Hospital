using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IPatientBusiness _patientBusiness;

        public MedicalRecordController(IPatientBusiness patientBusiness)
        {
            _patientBusiness = patientBusiness;
        }

        // GET: api/MedicalRecord
        [HttpGet]
        public async Task<IPaginatedList<PatientDto>> Get()
        {
            var result = await _patientBusiness.GetAll(0, 20);
            return result;
        }

        // GET: api/MedicalRecord/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MedicalRecord
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/MedicalRecord/5
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
