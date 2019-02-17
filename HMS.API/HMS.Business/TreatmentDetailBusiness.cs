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
using System;

namespace HMS.Business
{
    public class TreatmentDetailBusiness : ITreatmentDetailBusiness
    {
        private readonly IMapper _mapper;
        private readonly ITreatmentDetailRepository _treatmentDetailRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly ITreatmentDiseaseRepository _treatmentDiseaseRepository;
        private readonly IDiseaseRepository _diseaseRepository;

        public TreatmentDetailBusiness(IMapper mapper,
            ITreatmentDetailRepository treatmentDetailRepository,
            ITreatmentRepository treatmentRepository,
            IEmployeeRepository employeeRepository,
            IPatientRepository patientRepository,
            IMedicalRecordRepository medicalRecordRepository,
            ITreatmentDiseaseRepository treatmentDiseaseRepository,
            IDiseaseRepository diseaseRepository)
        {
            _mapper = mapper;
            _treatmentDetailRepository = treatmentDetailRepository;
            _treatmentRepository = treatmentRepository;
            _employeeRepository = employeeRepository;
            _patientRepository = patientRepository;
            _medicalRecordRepository = medicalRecordRepository;
            _treatmentDiseaseRepository = treatmentDiseaseRepository;
            _diseaseRepository = diseaseRepository;
        }

        public async Task<TreatmentDetailDto> Add(TreatmentDetailDto model)
        {
            var entity = _mapper.Map<TTreatmentDetail>(model);
            entity.Code = string.Empty;
            _treatmentDetailRepository.Add(entity);
            await _treatmentDetailRepository.SaveChangeAsync();
            var maxId = await _treatmentDetailRepository.Repo.MaxAsync(c => c.Id);
            entity.Code = $"DTCT-{(maxId + 1):D10}";
            await _treatmentDetailRepository.SaveChangeAsync();
            model.Id = entity.Id;
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            _treatmentDetailRepository.Delete(id);
            var recordUpdated = await _treatmentDetailRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }

        public Task<IPaginatedList<TreatmentDetailDto>> GetByMedicalRecordId(int medicalRecordId, int pageIndex, int pageSize)
        {
            var result = (from treatmentDetail in _treatmentDetailRepository.Repo.Where(c => c.MedicalRecordId == medicalRecordId && c.IsActived)
                          join treatment in _treatmentRepository.Repo on treatmentDetail.TreatmentId equals treatment.Id
                          join patient in _patientRepository.Repo on treatmentDetail.PatientId equals patient.Id
                          join medicalRecord in _medicalRecordRepository.Repo on treatmentDetail.MedicalRecordId equals medicalRecord.Id
                          join doctor in _employeeRepository.Repo on treatmentDetail.DoctorId equals doctor.Id
                          into leftDoctors
                          from doctor in leftDoctors.DefaultIfEmpty()
                          join nurse in _employeeRepository.Repo on treatmentDetail.NurseId equals nurse.Id
                          into leftNurses
                          from nurse in leftNurses.DefaultIfEmpty()
                          select new TreatmentDetailDto
                          {
                              Id = treatmentDetail.Id,
                              Code = treatmentDetail.Code,
                              PatientId = treatmentDetail.PatientId,
                              PatientFirstName = patient.FirstName,
                              PatientLastName = patient.LastName,
                              MedicalRecordId = treatmentDetail.MedicalRecordId,
                              MedicalRecordCode = medicalRecord.Code,
                              TreatmentId = treatmentDetail.TreatmentId,
                              TreatmentCode = treatment.Code,
                              StartDate = treatmentDetail.StartDate,
                              EndDate = treatmentDetail.EndDate,
                              DoctorId = treatmentDetail.DoctorId,
                              NurseId = treatmentDetail.NurseId,
                              Content = treatmentDetail.Content,
                              Result = treatmentDetail.Result,
                              Note = treatmentDetail.Note,
                              CreatedTime = treatmentDetail.CreatedTime,
                              CreatedBy = treatmentDetail.CreatedBy,
                              UpdatedTime = treatmentDetail.UpdatedTime,
                              UpdatedBy = treatmentDetail.UpdatedBy,
                              IsActived = treatmentDetail.IsActived,
                              IsDeleted = treatmentDetail.IsDeleted,
                              DoctorFirstName = doctor.FirstName,
                              DoctorLastName = doctor.LastName,
                              NurseFirstName = nurse.FirstName,
                              NurseLastName = nurse.LastName,
                          })
                          .OrderByDescending(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public Task<IPaginatedList<TreatmentDetailDto>> GetByTreatmentId(int treatmentId, int pageIndex, int pageSize)
        {
            var result = (from treatmentDetail in _treatmentDetailRepository.Repo.Where(c => c.TreatmentId == treatmentId && c.IsActived)
                          join treatment in _treatmentRepository.Repo on treatmentDetail.TreatmentId equals treatment.Id
                          join patient in _patientRepository.Repo.Where(c => c.IsActived) on treatmentDetail.PatientId equals patient.Id
                          join medicalRecord in _medicalRecordRepository.Repo.Where(c => c.IsActived) on treatmentDetail.MedicalRecordId equals medicalRecord.Id
                          join doctor in _employeeRepository.Repo on treatmentDetail.DoctorId equals doctor.Id
                          into leftDoctors
                          from doctor in leftDoctors.DefaultIfEmpty()
                          join nurse in _employeeRepository.Repo on treatmentDetail.NurseId equals nurse.Id
                          into leftNurses
                          from nurse in leftNurses.DefaultIfEmpty()
                          select new TreatmentDetailDto
                          {
                              Id = treatmentDetail.Id,
                              Code = treatmentDetail.Code,
                              PatientId = treatmentDetail.PatientId,
                              PatientFirstName = patient.FirstName,
                              PatientLastName = patient.LastName,
                              MedicalRecordId = treatmentDetail.MedicalRecordId,
                              MedicalRecordCode = medicalRecord.Code,
                              TreatmentId = treatmentDetail.TreatmentId,
                              TreatmentCode = treatment.Code,
                              StartDate = treatmentDetail.StartDate,
                              EndDate = treatmentDetail.EndDate,
                              DoctorId = treatmentDetail.DoctorId,
                              NurseId = treatmentDetail.NurseId,
                              Content = treatmentDetail.Content,
                              Result = treatmentDetail.Result,
                              Note = treatmentDetail.Note,
                              CreatedTime = treatmentDetail.CreatedTime,
                              CreatedBy = treatmentDetail.CreatedBy,
                              UpdatedTime = treatmentDetail.UpdatedTime,
                              UpdatedBy = treatmentDetail.UpdatedBy,
                              IsActived = treatmentDetail.IsActived,
                              IsDeleted = treatmentDetail.IsDeleted,
                              DoctorFirstName = doctor.FirstName,
                              DoctorLastName = doctor.LastName,
                              NurseFirstName = nurse.FirstName,
                              NurseLastName = nurse.LastName,
                          })
                          .OrderByDescending(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public Task<TreatmentDetailDto> GetById(int id)
        {
            var result = (from treatmentDetail in _treatmentDetailRepository.Repo.Where(c => c.Id == id && c.IsActived)
                          join treatment in _treatmentRepository.Repo on treatmentDetail.TreatmentId equals treatment.Id
                          join patient in _patientRepository.Repo on treatmentDetail.PatientId equals patient.Id
                          join medicalRecord in _medicalRecordRepository.Repo on treatmentDetail.MedicalRecordId equals medicalRecord.Id
                          join doctor in _employeeRepository.Repo on treatmentDetail.DoctorId equals doctor.Id
                          into leftDoctors
                          from doctor in leftDoctors.DefaultIfEmpty()
                          join nurse in _employeeRepository.Repo on treatmentDetail.NurseId equals nurse.Id
                          into leftNurses
                          from nurse in leftNurses.DefaultIfEmpty()
                          select new TreatmentDetailDto
                          {
                              Id = treatmentDetail.Id,
                              Code = treatmentDetail.Code,
                              PatientId = treatmentDetail.PatientId,
                              PatientFirstName = patient.FirstName,
                              PatientLastName = patient.LastName,
                              MedicalRecordId = treatment.MedicalRecordId,
                              MedicalRecordCode = medicalRecord.Code,
                              TreatmentId = treatmentDetail.TreatmentId,
                              TreatmentCode = treatment.Code,
                              StartDate = treatmentDetail.StartDate,
                              EndDate = treatmentDetail.EndDate,
                              DoctorId = treatmentDetail.DoctorId,
                              NurseId = treatmentDetail.NurseId,
                              Result = treatmentDetail.Result,
                              Content = treatmentDetail.Content,
                              Note = treatmentDetail.Note,
                              CreatedTime = treatmentDetail.CreatedTime,
                              CreatedBy = treatmentDetail.CreatedBy,
                              UpdatedTime = treatmentDetail.UpdatedTime,
                              UpdatedBy = treatmentDetail.UpdatedBy,
                              IsActived = treatmentDetail.IsActived,
                              IsDeleted = treatmentDetail.IsDeleted,
                              DoctorFirstName = doctor.FirstName,
                              DoctorLastName = doctor.LastName,
                              NurseFirstName = nurse.FirstName,
                              NurseLastName = nurse.LastName,
                          })
                          .FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(TreatmentDetailDto model)
        {
            var entity = await _treatmentDetailRepository.Repo.FirstOrDefaultAsync(c => c.Id == model.Id);
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.DoctorId = model.DoctorId;
            entity.NurseId = model.NurseId;
            entity.Result = model.Result;
            entity.Content = model.Content;
            entity.UpdatedBy = model.UpdatedBy;
            entity.UpdatedTime = model.UpdatedTime;
            entity.Note = model.Note;
            var recordUpdated = await _treatmentDetailRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }

        public Task<IPaginatedList<TreatmentDetailDto>> GetByPatientId(int patientId, int pageIndex, int pageSize)
        {
            var result = (from treatmentDetail in _treatmentDetailRepository.Repo.Where(c => c.PatientId == patientId && c.IsActived)
                          join treatment in _treatmentRepository.Repo on treatmentDetail.TreatmentId equals treatment.Id
                          join patient in _patientRepository.Repo.Where(c => c.IsActived) on treatmentDetail.PatientId equals patient.Id
                          join medicalRecord in _medicalRecordRepository.Repo.Where(c => c.IsActived) on treatmentDetail.MedicalRecordId equals medicalRecord.Id
                          join doctor in _employeeRepository.Repo on treatmentDetail.DoctorId equals doctor.Id
                          into leftDoctors
                          from doctor in leftDoctors.DefaultIfEmpty()
                          join nurse in _employeeRepository.Repo on treatmentDetail.NurseId equals nurse.Id
                          into leftNurses
                          from nurse in leftNurses.DefaultIfEmpty()
                          select new TreatmentDetailDto
                          {
                              Id = treatmentDetail.Id,
                              Code = treatmentDetail.Code,
                              PatientId = treatmentDetail.PatientId,
                              PatientFirstName = patient.FirstName,
                              PatientLastName = patient.LastName,
                              MedicalRecordId = treatmentDetail.MedicalRecordId,
                              MedicalRecordCode = medicalRecord.Code,
                              TreatmentId = treatmentDetail.TreatmentId,
                              TreatmentCode = treatment.Code,
                              StartDate = treatmentDetail.StartDate,
                              EndDate = treatmentDetail.EndDate,
                              DoctorId = treatmentDetail.DoctorId,
                              NurseId = treatmentDetail.NurseId,
                              Result = treatmentDetail.Result,
                              Content = treatmentDetail.Content,
                              Note = treatmentDetail.Note,
                              CreatedTime = treatmentDetail.CreatedTime,
                              CreatedBy = treatmentDetail.CreatedBy,
                              UpdatedTime = treatmentDetail.UpdatedTime,
                              UpdatedBy = treatmentDetail.UpdatedBy,
                              IsActived = treatmentDetail.IsActived,
                              IsDeleted = treatmentDetail.IsDeleted,
                              DoctorFirstName = doctor.FirstName,
                              DoctorLastName = doctor.LastName,
                              NurseFirstName = nurse.FirstName,
                              NurseLastName = nurse.LastName,
                          })
                          .OrderByDescending(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public Task<IPaginatedList<TreatmentDetailDto>> GetAll(int pageIndex, int pageSize)
        {

            var result = (from treatmentDetail in _treatmentDetailRepository.Repo.Where(c => c.IsActived)
                          join treatment in _treatmentRepository.Repo on treatmentDetail.TreatmentId equals treatment.Id
                          join patient in _patientRepository.Repo.Where(c => c.IsActived) on treatmentDetail.PatientId equals patient.Id
                          join medicalRecord in _medicalRecordRepository.Repo.Where(c => c.IsActived) on treatmentDetail.MedicalRecordId equals medicalRecord.Id
                          join doctor in _employeeRepository.Repo on treatmentDetail.DoctorId equals doctor.Id
                          into leftDoctors
                          from doctor in leftDoctors.DefaultIfEmpty()
                          join nurse in _employeeRepository.Repo on treatmentDetail.NurseId equals nurse.Id
                          into leftNurses
                          from nurse in leftNurses.DefaultIfEmpty()
                          select new TreatmentDetailDto
                          {
                              Id = treatmentDetail.Id,
                              Code = treatmentDetail.Code,
                              PatientId = treatmentDetail.PatientId,
                              PatientFirstName = patient.FirstName,
                              PatientLastName = patient.LastName,
                              MedicalRecordId = treatmentDetail.MedicalRecordId,
                              MedicalRecordCode = medicalRecord.Code,
                              TreatmentId = treatmentDetail.TreatmentId,
                              TreatmentCode = treatment.Code,
                              StartDate = treatmentDetail.StartDate,
                              EndDate = treatmentDetail.EndDate,
                              DoctorId = treatmentDetail.DoctorId,
                              NurseId = treatmentDetail.NurseId,
                              Result = treatmentDetail.Result,
                              Content = treatmentDetail.Content,
                              Note = treatmentDetail.Note,
                              CreatedTime = treatmentDetail.CreatedTime,
                              CreatedBy = treatmentDetail.CreatedBy,
                              UpdatedTime = treatmentDetail.UpdatedTime,
                              UpdatedBy = treatmentDetail.UpdatedBy,
                              IsActived = treatmentDetail.IsActived,
                              IsDeleted = treatmentDetail.IsDeleted,
                              DoctorFirstName = doctor.FirstName,
                              DoctorLastName = doctor.LastName,
                              NurseFirstName = nurse.FirstName,
                              NurseLastName = nurse.LastName,
                          })
                          .OrderByDescending(c => c.Id)
                          .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }
    }
}
