using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Business.Paginated;
using HMS.Common.Dtos.Address;
using HMS.Common.Dtos.User;
using HMS.Common.Enums;
using HMS.Common.Responses.User;
using HMS.Entities.Models;
using HMS.Repository.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HMS.Business
{
    public class AddressBusiness : IAddressBusiness
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;

        public AddressBusiness(IMapper mapper,
            ICountryRepository countryRepository,
            IProvinceRepository provinceRepository,
            IDistrictRepository districtRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
        }

        public Task<IPaginatedList<CountryDto>> GetCountries(int pageIndex, int pageSize)
        {
            return _countryRepository.Repo
                .Select(c => new CountryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Code = c.Code
                })
                .OrderBy(c => c.Name)
                .ToPaginatedListAsync(pageIndex, pageSize);
        }

        public Task<IPaginatedList<DistrictDto>> GetDistricts(int pageIndex, int pageSize)
        {
            return _districtRepository.Repo
                .Select(c => new DistrictDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Code = c.Code,
                    ProvinceId = c.ProvinceId
                })
                .OrderBy(c => c.Name)
                .ToPaginatedListAsync(pageIndex, pageSize);
        }

        public Task<IPaginatedList<ProvinceDto>> GetProvinces(int pageIndex, int pageSize)
        {
            return _provinceRepository.Repo
                .Select(c => new ProvinceDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Code = c.Code,
                    CountryId = c.CountryId
                })
                .OrderBy(c => c.Name)
                .ToPaginatedListAsync(pageIndex, pageSize);
        }
    }
}
