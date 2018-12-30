using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using HMS.Business.Interfaces;
using HMS.Entities.Models;
using HMS.Repository.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Business.Paginated;
using Microsoft.EntityFrameworkCore;
using HMS.Common.Dtos.Patient;
using HMS.Common.Filters;
using System.Linq.Expressions;
using System;

namespace HMS.Business
{
    public class PatientBusiness : IPatientBusiness
    {
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;

        public PatientBusiness(IMapper mapper, 
            IPatientRepository patientRepository,
            ICountryRepository countryRepository,
            IProvinceRepository provinceRepository,
            IDistrictRepository districtRepository)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
            _countryRepository = countryRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
        }

        public async Task<PatientDto> Add(PatientDto model)
        {
            model.Code = string.Empty;
            var entity = _patientRepository.Add(_mapper.Map<TPatient>(model));
            await _patientRepository.SaveChangeAsync();
            var maxId = await _patientRepository.Repo.MaxAsync(c => c.Id);
            entity.Code = $"BN-{(maxId + 1):D8}";
            await _patientRepository.SaveChangeAsync();
            model.Id = entity.Id;

            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _patientRepository.Delete(id);
            var recordUpdated = await _patientRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }

        public Task<IPaginatedList<PatientDto>> GetAll(int pageIndex = 0, int pageSize = 20)
        {
            var result = GetAll(null, pageIndex, pageSize);
            return result;
        }

        public Task<IPaginatedList<PatientDto>> GetAll(PatientFilter filter)
        {
            Expression<Func<TPatient, bool>> expression = null;
            if (!string.IsNullOrEmpty(filter.Code))
            {
                expression = c => c.Code.Contains(filter.Code);
            }
            else if (!string.IsNullOrEmpty(filter.Name))
            {
                expression = c => (c.FirstName + " " + c.LastName).Contains(filter.Name);
            }
            else if (!string.IsNullOrEmpty(filter.Phone))
            {
                expression = c => c.Phone.Contains(filter.Phone);
            }
            else if (!string.IsNullOrEmpty(filter.IdentifyCardNo))
            {
                expression = c => c.IdentifyCardNo.Contains(filter.IdentifyCardNo);
            }
            return GetAll(expression, filter.PageIndex, filter.PageSize);
        }

        public async Task<PatientDto> GetById(int id)
        {
            var result = await _patientRepository.Repo.Where(c => c.Id == id)
                .Include(c => c.Country)
                .Include(c => c.Province)
                .Include(c => c.District)
                .Include(c => c.NativeCountry)
                .Include(c => c.NativeProvince)
                .Include(c => c.NativeDistrict)
                .FirstOrDefaultAsync();
            return _mapper.Map<PatientDto>(result);
        }

        public async Task<bool> Update(PatientDto model)
        {
            _patientRepository.Update(_mapper.Map<TPatient>(model));
            var recordUpdated = await _patientRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }

        private Task<IPaginatedList<PatientDto>> GetAll(Expression<Func<TPatient, bool>> expression, int pageIndex = 0, int pageSize = 20)
        {
            var patientRepo = _patientRepository.Repo;
            if(expression != null)
            {
                patientRepo = patientRepo.Where(expression);
            }
            var result = (from patient in patientRepo
                          join country in _countryRepository.Repo on patient.CountryId equals country.Id
                          join province in _provinceRepository.Repo on patient.ProvinceId equals province.Id
                          join district in _districtRepository.Repo on patient.DistrictId equals district.Id
                          join nCountry in _countryRepository.Repo on patient.NativeCountryId equals nCountry.Id
                          join nProvince in _provinceRepository.Repo on patient.NativeProvinceId equals nProvince.Id
                          join nDistrict in _districtRepository.Repo on patient.NativeDistrictId equals nDistrict.Id
                          select new PatientDto
                          {
                              Id = patient.Id,
                              Code = patient.Code,
                              FirstName = patient.FirstName,
                              LastName = patient.LastName,
                              CountryId = patient.CountryId,
                              ProvinceId = patient.ProvinceId,
                              DistrictId = patient.DistrictId,
                              Address = patient.Address,
                              NativeCountryId = patient.NativeCountryId,
                              NativeProvinceId = patient.NativeProvinceId,
                              NativeDistrictId = patient.NativeDistrictId,
                              NativeAddress = patient.NativeAddress,
                              Sex = patient.Sex,
                              Phone = patient.Phone,
                              Email = patient.Email,
                              Birthday = patient.Birthday,
                              IdentifyCardNo = patient.IdentifyCardNo,
                              DateOfIssue = patient.DateOfIssue,
                              PlaceOfIssue = patient.PlaceOfIssue,
                              ImageUrl = patient.ImageUrl,
                              FirstRelativeName = patient.FirstRelativeName,
                              FirstRelativePhone = patient.FirstRelativePhone,
                              FirstRelativeIdentifyCardNo = patient.FirstRelativeIdentifyCardNo,
                              FirstRelativeDescription = patient.FirstRelativeDescription,
                              SecondRelativeName = patient.SecondRelativeName,
                              SecondRelativePhone = patient.SecondRelativePhone,
                              SecondRelativeIdentifyCardNo = patient.SecondRelativeIdentifyCardNo,
                              SecondRelativeDescription = patient.SecondRelativeDescription,
                              Description = patient.Description,
                              CreatedTime = patient.CreatedTime,
                              CreatedBy = patient.CreatedBy,
                              UpdatedTime = patient.UpdatedTime,
                              UpdatedBy = patient.UpdatedBy,
                              IsActived = patient.IsActived,
                              IsDeleted = patient.IsDeleted,
                              CountryName = country.Name,
                              ProvinceName = province.Name,
                              DistrictName = district.Name,
                              NativeCountryName = nCountry.Name,
                              NativeProvinceName = nProvince.Name,
                              NativeDistrictName = nDistrict.Name,
                          })
                          .OrderByDescending(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }
    }
}
