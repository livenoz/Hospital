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
using HMS.Common.Enums;

namespace HMS.Business
{
    public class MedicalRecordBusiness : IMedicalRecordBusiness
    {
        private readonly IMapper _mapper;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicalRecordStatusRepository _medicalRecordStatusRepository;

        public MedicalRecordBusiness(IMapper mapper,
            IMedicalRecordRepository medicalRecordRepository,
            IPatientRepository patientRepository,
            IMedicalRecordStatusRepository medicalRecordStatusRepository)
        {
            _mapper = mapper;
            _medicalRecordRepository = medicalRecordRepository;
            _patientRepository = patientRepository;
            _medicalRecordStatusRepository = medicalRecordStatusRepository;
        }

        public async Task<MedicalRecordDto> Add(MedicalRecordDto model)
        {
            model.Code = string.Empty;
            var entity = _medicalRecordRepository.Add(_mapper.Map<TMedicalRecord>(model));
            await _medicalRecordRepository.SaveChangeAsync();
            var maxId = await _medicalRecordRepository.Repo.MaxAsync(c => c.Id);
            entity.Code = $"BA-{(maxId + 1):D10}";
            await _medicalRecordRepository.SaveChangeAsync();
            model.Id = entity.Id;
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _medicalRecordRepository.Delete(id);
            var recordUpdated = await _medicalRecordRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }

        public async Task<MedicalRecordDto> GetById(int id)
        {
            var result = await _medicalRecordRepository.Repo.Where(c => c.Id == id && c.IsActived)
                .Include(c => c.Status)
                .FirstOrDefaultAsync();
            return _mapper.Map<MedicalRecordDto>(result);
        }

        public async Task<bool> Update(MedicalRecordDto model)
        {
            _medicalRecordRepository.Update(_mapper.Map<TMedicalRecord>(model));
            var recordUpdated = await _medicalRecordRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }

        public Task<IPaginatedList<MedicalRecordDto>> GetByPatientId(int patientId, int pageIndex, int pageSize)
        {
            var result = (from medical in _medicalRecordRepository.Repo
                          .Where(c => c.PatientId == patientId && c.IsActived)
                          join status in _medicalRecordStatusRepository.Repo on medical.StatusId equals status.Id
                          select new MedicalRecordDto
                          {
                              Id = medical.Id,
                              Code = medical.Code,
                              PatientId = medical.PatientId,
                              StartDate = medical.StartDate,
                              EndDate = medical.EndDate,
                              Reason = medical.Reason,
                              StatusId = medical.StatusId,
                              StatusName = status.Name,
                              CreatedTime = medical.CreatedTime,
                              CreatedBy = medical.CreatedBy,
                              UpdatedTime = medical.UpdatedTime,
                              UpdatedBy = medical.UpdatedBy,
                              IsActived = medical.IsActived,
                              IsDeleted = medical.IsDeleted,
                          })
                          .OrderByDescending(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public Task<IPaginatedList<MedicalRecordDto>> GetAll(int pageIndex, int pageSize)
        {
            var result = (from medical in _medicalRecordRepository.Repo.Where(c => c.IsActived)
                          join patient in _patientRepository.Repo on medical.PatientId equals patient.Id
                          join status in _medicalRecordStatusRepository.Repo on medical.StatusId equals status.Id
                          select new MedicalRecordDto
                          {
                              Id = medical.Id,
                              Code = medical.Code,
                              PatientId = medical.PatientId,
                              PatientFirstName = patient.FirstName,
                              PatientLastName = patient.LastName,
                              StartDate = medical.StartDate,
                              EndDate = medical.EndDate,
                              Reason = medical.Reason,
                              StatusId = medical.StatusId,
                              StatusName = status.Name,
                              CreatedTime = medical.CreatedTime,
                              CreatedBy = medical.CreatedBy,
                              UpdatedTime = medical.UpdatedTime,
                              UpdatedBy = medical.UpdatedBy,
                              IsActived = medical.IsActived,
                              IsDeleted = medical.IsDeleted,
                          })
                          .OrderByDescending(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public Task<IPaginatedList<MedicalRecordDto>> GetBeingTreated(int pageIndex, int pageSize)
        {
            var result = (from medical in _medicalRecordRepository.Repo.Where(c => c.IsActived && c.StatusId == (int)EMedicalRecordStatus.BeingTreated)
                          join patient in _patientRepository.Repo on medical.PatientId equals patient.Id
                          join status in _medicalRecordStatusRepository.Repo on medical.StatusId equals status.Id
                          select new MedicalRecordDto
                          {
                              Id = medical.Id,
                              Code = medical.Code,
                              PatientId = medical.PatientId,
                              PatientFirstName = patient.FirstName,
                              PatientLastName = patient.LastName,
                              StartDate = medical.StartDate,
                              EndDate = medical.EndDate,
                              Reason = medical.Reason,
                              StatusId = medical.StatusId,
                              StatusName = status.Name,
                              CreatedTime = medical.CreatedTime,
                              CreatedBy = medical.CreatedBy,
                              UpdatedTime = medical.UpdatedTime,
                              UpdatedBy = medical.UpdatedBy,
                              IsActived = medical.IsActived,
                              IsDeleted = medical.IsDeleted,
                          })
                          .OrderByDescending(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }
    }
}
