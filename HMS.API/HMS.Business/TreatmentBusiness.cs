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
    public class TreatmentBusiness : ITreatmentBusiness
    {
        private readonly IMapper _mapper;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public TreatmentBusiness(IMapper mapper,
            ITreatmentRepository treatmentRepository,
            IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _treatmentRepository = treatmentRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<TreatmentDto> Add(TreatmentDto model)
        {
            var entity = _treatmentRepository.Add(_mapper.Map<TTreatment>(model));
            await _treatmentRepository.SaveChangeAsync();
            model.Id = entity.Id;
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _treatmentRepository.Delete(id);
            var recordUpdated = await _treatmentRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }

        public async Task<IPaginatedList<TreatmentDto>> GetByMedicalRecordId(int medicalRecordId, int pageIndex, int pageSize)
        {
            var result = await (from treatment in _treatmentRepository.Repo.Where(c => c.IsActived)
                                join doctor in _employeeRepository.Repo on treatment.DoctorId equals doctor.Id
                                into leftDoctors
                                from doctor in leftDoctors.DefaultIfEmpty()
                                join nurse in _employeeRepository.Repo on treatment.NurseId equals nurse.Id
                                into leftNurses
                                from nurse in leftNurses.DefaultIfEmpty()
                                select new TreatmentDto
                                {
                                    Id = treatment.Id,
                                    MedicalRecordId = treatment.MedicalRecordId,
                                    StartDate = treatment.StartDate,
                                    EndDate = treatment.EndDate,
                                    DoctorId = treatment.DoctorId,
                                    NurseId = treatment.NurseId,
                                    Content = treatment.Content,
                                    Note = treatment.Note,
                                    CreatedTime = treatment.CreatedTime,
                                    CreatedBy = treatment.CreatedBy,
                                    UpdatedTime = treatment.UpdatedTime,
                                    UpdatedBy = treatment.UpdatedBy,
                                    IsActived = treatment.IsActived,
                                    IsDeleted = treatment.IsDeleted,
                                    DoctorFirstName = doctor.FirstName,
                                    DoctorLastName = doctor.LastName,
                                    NurseFirstName = nurse.FirstName,
                                    NurseLastName = nurse.LastName,
                                })
                                .OrderByDescending(c => c.Id)
                                .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public async Task<TreatmentDto> GetById(int id)
        {
            //var result = await _treatmentRepository.Repo.Where(c => c.Id == id)
            //    .Include(c => c.Country)
            //    .Include(c => c.Province)
            //    .Include(c => c.District)
            //    .Include(c => c.NativeCountry)
            //    .Include(c => c.NativeProvince)
            //    .Include(c => c.NativeDistrict)
            //    .FirstOrDefaultAsync();
            //return _mapper.Map<TreatmentDto>(result);
            return null;
        }

        public async Task<bool> Update(TreatmentDto model)
        {
            _treatmentRepository.Update(_mapper.Map<TTreatment>(model));
            var recordUpdated = await _treatmentRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }
    }
}
