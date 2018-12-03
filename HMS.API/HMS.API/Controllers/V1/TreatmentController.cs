using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.API.Attributes;
using HMS.API.Extensions;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Patient;
using HMS.Common.Dtos.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]/[action]")]
    [BearerAuthorize]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly ITreatmentBusiness _treatmentBusiness;

        public TreatmentController(IHttpContextAccessor httpContextAccessor,
            ITreatmentBusiness treatmentBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _treatmentBusiness = treatmentBusiness;
        }
        // GET: api/Treatment
        [HttpGet]
        public async Task<IPaginatedList<TreatmentDto>> Get()
        {
            throw new NotImplementedException();
        }

        // GET: api/Treatment/5
        [HttpGet("{id}")]
        public async Task<TreatmentDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Treatment
        [HttpPost]
        public async Task<int> Post(TreatmentDto model)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Treatment/5
        [HttpPut("{id}")]
        public async Task<bool> Put(TreatmentDto model)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<IPaginatedList<TreatmentDto>> GetByMedicalRecord(
            int medicalRecordId, int pageIndex, int pageSize)
        {
            return _treatmentBusiness.GetByMedicalRecordId(medicalRecordId, pageIndex, pageSize);
        }
    }
}
