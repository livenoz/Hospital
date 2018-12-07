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
            var entity = _patientRepository.Add(_mapper.Map<TPatient>(model));
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

        public async Task<IPaginatedList<PatientDto>> GetAll(int pageIndex = 0, int pageSize = 20)
        {
            var result = await (from patient in _patientRepository.Repo.Where(c => c.IsActived)
                                select new PatientDto
                                {
                                    Id = patient.Id,
                                    Code = patient.Code,
                                    FirstName = patient.FirstName,
                                    LastName = patient.LastName,
                                    FullName = string.Concat(patient.FirstName, " ", patient.LastName),
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
                                    FatherName = patient.FatherName,
                                    FatherIdentifyCardNo = patient.FatherIdentifyCardNo,
                                    MotherName = patient.MotherName,
                                    MotherIdentifyCardNo = patient.MotherIdentifyCardNo,
                                    Description = patient.Description,
                                    CreatedTime = patient.CreatedTime,
                                    CreatedBy = patient.CreatedBy,
                                    UpdatedTime = patient.UpdatedTime,
                                    UpdatedBy = patient.UpdatedBy,
                                    IsActived = patient.IsActived,
                                    IsDeleted = patient.IsDeleted
                                })
                                .OrderByDescending(c => c.Id)
                                .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
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
    }
}
