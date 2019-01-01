using System;
using System.Threading.Tasks;
using HMS.API.Attributes;
using HMS.API.Extensions;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Constants;
using HMS.Common.Dtos.Patient;
using HMS.Common.Dtos.User;
using HMS.Common.Filters;
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
        public Task<IPaginatedList<PatientDto>> Get(string code, string value,
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var filter = new PatientFilter
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Code = string.Equals("Code", code, StringComparison.OrdinalIgnoreCase) ? value : string.Empty,
                Name = string.Equals("Name", code, StringComparison.OrdinalIgnoreCase) ? value : string.Empty,
                Phone = string.Equals("Phone", code, StringComparison.OrdinalIgnoreCase) ? value : string.Empty,
                IdentifyCardNo = string.Equals("IdentifyCardNo", code, StringComparison.OrdinalIgnoreCase) ? value : string.Empty,
            };
            return _patientBusiness.GetAll(filter);
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public Task<PatientDto> Get(int id)
        {
            return _patientBusiness.GetById(id);
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
                model.IsActived = true;
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
        public Task<bool> Delete(int id)
        {
            var result = _patientBusiness.Delete(id);
            return result;
        }
    }
}
