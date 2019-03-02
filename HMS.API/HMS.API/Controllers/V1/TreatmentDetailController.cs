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
    public class TreatmentDetailController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly ITreatmentDetailBusiness _treatmentDetailBusiness;

        public TreatmentDetailController(IHttpContextAccessor httpContextAccessor,
            ITreatmentDetailBusiness treatmentDetailBusiness)
        {
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
            _treatmentDetailBusiness = treatmentDetailBusiness;
        }


        // GET: api/Treatment
        [HttpGet]
        public Task<IPaginatedList<TreatmentDetailDto>> Get(
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _treatmentDetailBusiness.GetAll(pageIndex, pageSize);
        }

        // GET: api/Treatment/5
        [HttpGet("{id}")]
        public Task<TreatmentDetailDto> Get(int id)
        {
            return _treatmentDetailBusiness.GetById(id);
        }

        // POST: api/Treatment
        [HttpPost]
        public async Task<int> Post(TreatmentDetailDto model)
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
                var modelInsert = await _treatmentDetailBusiness.Add(model);
                result = modelInsert.Id;
            }
            return result;
        }

        // PUT: api/Treatment/5
        [HttpPut("{id}")]
        public async Task<bool> Put(TreatmentDetailDto model)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                var dateTimeUtcNow = DateTime.Now;
                model.UpdatedBy = _authenticationDto.UserId;
                model.UpdatedTime = dateTimeUtcNow;
                result = await _treatmentDetailBusiness.Update(model);
            }
            return result;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("GetByMedicalRecordId/{medicalRecordId}")]
        public Task<IPaginatedList<TreatmentDetailDto>> GetByMedicalRecordId(int medicalRecordId,
                int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _treatmentDetailBusiness.GetByMedicalRecordId(medicalRecordId, pageIndex, pageSize);
        }

        [HttpGet("GetByPatientId")]
        public Task<IPaginatedList<TreatmentDetailDto>> GetByPatientId(int patientId,
                int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _treatmentDetailBusiness.GetByPatientId(patientId, pageIndex, pageSize);
        }

        [HttpGet("GetByTreatmentId/{treatmentId}")]
        public Task<IPaginatedList<TreatmentDetailDto>> GetByTreatmentId(int treatmentId,
                int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _treatmentDetailBusiness.GetByTreatmentId(treatmentId, pageIndex, pageSize);
        }
    }
}
