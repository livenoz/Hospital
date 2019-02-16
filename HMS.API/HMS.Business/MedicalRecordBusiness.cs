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
using HMS.Common.Filters;
using System.Linq.Expressions;
using System;

namespace HMS.Business
{
    public class MedicalRecordBusiness : IMedicalRecordBusiness
    {
        private readonly IMapper _mapper;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicalRecordStatusRepository _medicalRecordStatusRepository;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly ITreatmentDiseaseRepository _treatmentDiseaseRepository;

        public MedicalRecordBusiness(IMapper mapper,
            IMedicalRecordRepository medicalRecordRepository,
            IPatientRepository patientRepository,
            IMedicalRecordStatusRepository medicalRecordStatusRepository,
            IDiseaseRepository diseaseRepository,
            ITreatmentDiseaseRepository treatmentDiseaseRepository)
        {
            _mapper = mapper;
            _medicalRecordRepository = medicalRecordRepository;
            _patientRepository = patientRepository;
            _medicalRecordStatusRepository = medicalRecordStatusRepository;
            _diseaseRepository = diseaseRepository;
            _treatmentDiseaseRepository = treatmentDiseaseRepository;
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
            Expression<Func<TMedicalRecord, bool>> expression = c => c.IsActived;
            return GetAll(expression, null, pageIndex, pageSize);
        }

        private Task<IPaginatedList<MedicalRecordDto>> GetAll(
            Expression<Func<TMedicalRecord, bool>> medicalRecordExpression,
            Expression<Func<TPatient, bool>> patientExpression,
            int pageIndex, int pageSize)
        {
            var medicalRecordRepo = _medicalRecordRepository.Repo;
            if (medicalRecordExpression != null)
            {
                medicalRecordRepo = medicalRecordRepo.Where(medicalRecordExpression);
            }

            var patientRepo = _patientRepository.Repo;
            if (patientExpression != null)
            {
                patientRepo = patientRepo.Where(patientExpression);
            }

            var result = (from medical in medicalRecordRepo
                          join patient in patientRepo on medical.PatientId equals patient.Id
                          join status in _medicalRecordStatusRepository.Repo on medical.StatusId equals status.Id
                          join treatmentDisease in
                              (from treatmentDisease in _treatmentDiseaseRepository.Repo
                               join disease in _diseaseRepository.Repo on treatmentDisease.DiseaseId equals disease.Id
                               select new
                               {
                                   treatmentDisease.MedicalRecordId,
                                   disease.Name
                               }) on medical.Id equals treatmentDisease.MedicalRecordId
                              into treatmentDiseases
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
                              Diagnose = String.Join(", ", treatmentDiseases.Select(c => c.Name))
                          })
                          .OrderByDescending(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public Task<IPaginatedList<MedicalRecordDto>> GetAll(MedicalRecordFilter filter)
        {
            Expression<Func<TMedicalRecord, bool>> medicalRecordExpression = null;
            Expression<Func<TPatient, bool>> patientExpression = null;
            if (!string.IsNullOrEmpty(filter.Code))
            {
                medicalRecordExpression = c => c.Code.Contains(filter.Code);
            }
            else if (!string.IsNullOrEmpty(filter.PatientCode))
            {
                patientExpression = c => c.Code.Contains(filter.Code);
            }
            else if (!string.IsNullOrEmpty(filter.PatientName))
            {
                patientExpression = c => (c.FirstName + " " + c.LastName).Contains(filter.PatientName);
            }
            else if (!string.IsNullOrEmpty(filter.IdentifyCardNo))
            {
                patientExpression = c => c.IdentifyCardNo.Contains(filter.IdentifyCardNo);
            }
            else
            {
                medicalRecordExpression = c => c.IsActived;
            }
            return GetAll(medicalRecordExpression, patientExpression, filter.PageIndex, filter.PageSize);
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

        public async Task<bool> SetActive(int id, bool isActive)
        {
            var result = false;
            var medicalRecord = await _medicalRecordRepository.Repo.FirstOrDefaultAsync(c => c.Id == id);
            if (medicalRecord != null)
            {
                medicalRecord.IsActived = isActive;
                await _medicalRecordRepository.SaveChangeAsync();
                result = true;
            }
            return result;
        }
    }
}
