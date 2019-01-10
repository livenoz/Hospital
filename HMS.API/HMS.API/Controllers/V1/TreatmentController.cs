using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.API.Attributes;
using HMS.API.Extensions;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Constants;
using HMS.Common.Dtos.Patient;
using HMS.Common.Dtos.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
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
        public Task<IPaginatedList<TreatmentDto>> Get(
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _treatmentBusiness.GetAll(pageIndex, pageSize);
        }

        // GET: api/Treatment/5
        [HttpGet("{id}")]
        public Task<TreatmentDto> Get(int id)
        {
            return _treatmentBusiness.GetById(id);
        }

        // POST: api/Treatment
        [HttpPost]
        public async Task<int> Post(TreatmentDto model)
        {
            var result = 0;
            if (ModelState.IsValid)
            {
                var dateTimeUtcNow = DateTime.Now;
                model.CreatedBy = _authenticationDto.UserId;
                model.CreatedTime = dateTimeUtcNow;
                model.UpdatedBy = _authenticationDto.UserId;
                model.UpdatedTime = dateTimeUtcNow;
                var modelInsert = await _treatmentBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }

        // PUT: api/Treatment/5
        [HttpPut("{id}")]
        public async Task<bool> Put(TreatmentDto model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                var dateTimeUtcNow = DateTime.Now;
                model.UpdatedBy = _authenticationDto.UserId;
                model.UpdatedTime = dateTimeUtcNow;
                result = await _treatmentBusiness.Update(model);
            }
            return result;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        [Route("api/v1/[controller]/GetByMedicalRecordId")]
        public Task<IPaginatedList<TreatmentDto>> GetByMedicalRecordId(int medicalRecordId,
                int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _treatmentBusiness.GetByMedicalRecordId(medicalRecordId, pageIndex, pageSize);
        }

        [Route("api/v1/[controller]/GetByPatientId")]
        public Task<IPaginatedList<TreatmentDto>> GetByPatientId(int patientId,
                int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _treatmentBusiness.GetByPatientId(patientId, pageIndex, pageSize);
        }
    }
}
