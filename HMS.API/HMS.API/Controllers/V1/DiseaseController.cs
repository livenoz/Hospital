using System;
using System.Threading.Tasks;
using AutoMapper;
using HMS.API.Attributes;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Constants;
using HMS.Common.Dtos.Employee;
using HMS.Common.Dtos.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IDiseaseBusiness _diseaseBusiness;

        public DiseaseController(ILogger<DiseaseController> logger,
            IMapper mapper,
            IDiseaseBusiness diseaseBusiness)
        {
            _logger = logger;
            _mapper = mapper;
            _diseaseBusiness = diseaseBusiness;
        }
        // GET: api/Disease
        [HttpGet]
        public Task<IPaginatedList<DiseaseDto>> Get(
            int pageIndex = Constant.PAGE_INDEX_DEFAULT, int pageSize = Constant.PAGE_SIZE_DEFAULT)
        {
            return _diseaseBusiness.GetAll(pageIndex, pageSize);
        }

        // GET: api/Disease/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Disease
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Disease/5
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
