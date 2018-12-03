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
    public class PrescriptionController : ControllerBase
    {
        // GET: api/Prescription
        [HttpGet]
        public async Task<IPaginatedList<PrescriptionDto>> Get(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        // GET: api/Prescription/5
        [HttpGet("{id}")]
        public async Task<PrescriptionDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Prescription
        [HttpPost]
        public async Task<int> Post(PrescriptionDto model)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Prescription/5
        [HttpPut("{id}")]
        public async Task<bool> Put(PrescriptionDto model)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IPaginatedList<PrescriptionDto>> GetPrescriptionByTreatmentId(int treatmentId, 
            int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }


        public async Task<IPaginatedList<PrescriptionDetailDto>> GetPrescriptionDetailsByPrescriptionId(
            int prescriptionId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
