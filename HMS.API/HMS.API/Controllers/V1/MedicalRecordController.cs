using System;
using System.Threading.Tasks;
using HMS.API.Attributes;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Patient;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        public MedicalRecordController()
        {
        }

        // GET: api/MedicalRecord
        [HttpGet]
        public async Task<IPaginatedList<MedicalRecordDto>> Get()
        {
            throw new NotImplementedException();
        }

        // GET: api/MedicalRecord/5
        [HttpGet("{id}")]
        public async Task<MedicalRecordDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/MedicalRecord
        [HttpPost]
        public async Task<int> Post(MedicalRecordDto model)
        {
            throw new NotImplementedException();
        }

        // PUT: api/MedicalRecord/5
        [HttpPut("{id}")]
        public async Task<bool> Put(MedicalRecordDto model)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IPaginatedList<MedicalRecordDto>> GetMedicalRecordByPatientId(int patientId)
        {
            throw new NotImplementedException();
        }
    }
}
