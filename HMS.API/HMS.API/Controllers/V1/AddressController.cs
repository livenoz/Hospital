using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HMS.API.Attributes;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Address;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]/[action]")]
    [BearerAuthorize]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAddressBusiness _addressBusiness;

        public AddressController(ILogger<AddressController> logger,
            IMapper mapper,
            IAddressBusiness addressBusiness)
        {
            _logger = logger;
            _mapper = mapper;
            _addressBusiness = addressBusiness;
        }

        public Task<IPaginatedList<CountryDto>> GetCountries(int pageIndex, int pageSize)
        {
            return _addressBusiness.GetCountries(pageIndex, pageSize);
        }

        public Task<IPaginatedList<ProvinceDto>> GetProvinces(int pageIndex, int pageSize)
        {
            return _addressBusiness.GetProvinces(pageIndex, pageSize);
        }

        public Task<IPaginatedList<DistrictDto>> GetDistricts(int pageIndex, int pageSize)
        {
            return _addressBusiness.GetDistricts(pageIndex, pageSize);
        }

    }
}
