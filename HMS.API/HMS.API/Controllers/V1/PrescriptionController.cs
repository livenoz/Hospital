using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HMS.API.Attributes;
using HMS.API.Extensions;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Constants;
using HMS.Common.Dtos.Patient;
using HMS.Common.Dtos.Patient.Parameters;
using HMS.Common.Dtos.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly AuthenticationDto _authenticationDto;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPrescriptionBusiness _prescriptionBusiness;

        public PrescriptionController(IHttpContextAccessor httpContextAccessor, 
            ILogger<DiseaseController> logger,
            IMapper mapper,
            IPrescriptionBusiness prescriptionBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _mapper = mapper;
            _prescriptionBusiness = prescriptionBusiness;
            _authenticationDto = httpContextAccessor.HttpContext.User.ToAuthenticationDto();
        }

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
        public async Task<List<int>> Post(List<PrescriptionDetailDto> models)
        {
            List<int> result = new List<int>();
            if (ModelState.IsValid && models != null && models.Any())
            {
                var dateTimeUtcNow = DateTime.Now;

                foreach (var model in models)
                {
                    model.CreatedBy = _authenticationDto.UserId;
                    model.CreatedTime = dateTimeUtcNow;
                    model.UpdatedBy = _authenticationDto.UserId;
                    model.UpdatedTime = dateTimeUtcNow;
                    model.IsActived = true;
                }

                var ids = await _prescriptionBusiness.Add(models);
                result.AddRange(ids);
            }
            return result;
        }

        // PUT: api/Prescription/5
        [HttpPut("{treatmentDetailId}")]
        public async Task<bool> Put(UpdatingPrescriptionsPrameters @params)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                var dateTimeUtcNow = DateTime.Now;
                foreach (var model in @params.PrescriptionDetails)
                {
                    model.CreatedBy = _authenticationDto.UserId;
                    model.CreatedTime = dateTimeUtcNow;
                    model.UpdatedBy = _authenticationDto.UserId;
                    model.UpdatedTime = dateTimeUtcNow;
                    model.IsActived = true;
                }
                result = await _prescriptionBusiness.Update(@params.TreatmentDetailId, @params.PrescriptionDetails);
            }
            return result;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("ByTreatmentDetailId")]
        public Task<IPaginatedList<PrescriptionDetailDto>> GetByTreatmentDetailId(int treatmentDetailId,
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _prescriptionBusiness.GetByTreatmentDetailId(treatmentDetailId);
        }

        [HttpGet("drugs")]
        public Task<IPaginatedList<DrugDto>> GetDrugs(
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _prescriptionBusiness.GetDrugs(pageIndex, pageSize);
        }

        [HttpGet("units")]
        public Task<IPaginatedList<UnitDto>> GetUnits(
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _prescriptionBusiness.GetUnits(pageIndex, pageSize);
        }
    }
}
