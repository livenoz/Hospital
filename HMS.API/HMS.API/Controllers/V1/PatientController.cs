using System;
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
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IPatientBusiness _patientBusiness;

        public PatientController(IHttpContextAccessor httpContextAccessor, 
            IPatientBusiness patientBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _patientBusiness = patientBusiness;
        }

        // GET: api/Patient
        [HttpGet]
        public async Task<IPaginatedList<PatientDto>> Get(int pageIndex, int pageSize)
        {
            return await _patientBusiness.GetAll(pageIndex, pageSize);
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<PatientDto> Get(int id)
        {
            return await _patientBusiness.GetById(id);
        }

        // POST: api/Patient
        [HttpPost]
        public async Task<int> Post(PatientDto model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                var dateTimeUtcNow = DateTime.UtcNow;
                model.CreatedBy = _authenticationDto.UserId;
                model.CreatedTime = dateTimeUtcNow;
                model.UpdatedBy = _authenticationDto.UserId;
                model.UpdatedTime = dateTimeUtcNow;
                var modelInsert = await _patientBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public async Task<bool> Put(PatientDto model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                var dateTimeUtcNow = DateTime.UtcNow;
                model.UpdatedBy = _authenticationDto.UserId;
                model.UpdatedTime = dateTimeUtcNow;
                result = await _patientBusiness.Update(model);
            }
            return result;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            var result = await _patientBusiness.Delete(id);
            return result;
        }
    }
}
