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
    public class MedicalRecordController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IMedicalRecordBusiness _medicalRecordBusiness;

        public MedicalRecordController(IHttpContextAccessor httpContextAccessor,
            IMedicalRecordBusiness medicalRecordBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _medicalRecordBusiness = medicalRecordBusiness;
        }

        // GET: api/MedicalRecord
        [HttpGet]
        public Task<IPaginatedList<MedicalRecordDto>> Get(string code, string value,
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            var filter = new MedicalRecordFilter
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Code = string.Equals("Code", code, StringComparison.OrdinalIgnoreCase) ? value : string.Empty,
                PatientCode = string.Equals("PatientCode", code, StringComparison.OrdinalIgnoreCase) ? value : string.Empty,
                PatientName = string.Equals("PatientName", code, StringComparison.OrdinalIgnoreCase) ? value : string.Empty,
                IdentifyCardNo = string.Equals("IdentifyCardNo", code, StringComparison.OrdinalIgnoreCase) ? value : string.Empty,
            };
            return _medicalRecordBusiness.GetAll(filter);
        }

        // GET: api/MedicalRecord/5
        [HttpGet("{id}")]
        public Task<MedicalRecordDto> Get(int id)
        {
            return _medicalRecordBusiness.GetById(id);
        }

        // POST: api/MedicalRecord
        [HttpPost]
        public async Task<int> Post(MedicalRecordDto model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                var dateTimeUtcNow = DateTime.Now;
                model.CreatedBy = _authenticationDto.UserId;
                model.CreatedTime = dateTimeUtcNow;
                model.UpdatedBy = _authenticationDto.UserId;
                model.UpdatedTime = dateTimeUtcNow;
                model.IsActived = true;
                var modelInsert = await _medicalRecordBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }

        // PUT: api/MedicalRecord/5
        [HttpPut("{id}")]
        public async Task<bool> Put(MedicalRecordDto model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                var dateTimeUtcNow = DateTime.Now;
                model.UpdatedBy = _authenticationDto.UserId;
                model.UpdatedTime = dateTimeUtcNow;
                result = await _medicalRecordBusiness.Update(model);
            }
            return result;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("bypatientid")]
        public Task<IPaginatedList<MedicalRecordDto>> GetByPatientId(int patientId, 
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _medicalRecordBusiness.GetByPatientId(patientId, pageIndex, pageSize);
        }


        // GET: api/MedicalRecord
        [HttpGet("beingtreated")]
        public Task<IPaginatedList<MedicalRecordDto>> GetBeingTreated(
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _medicalRecordBusiness.GetBeingTreated(pageIndex, pageSize);
        }

        [HttpPut("active")]
        public Task<bool> Put(int id, bool isActive)
        {
            return _medicalRecordBusiness.SetActive(id, isActive);
        }
    }
}
