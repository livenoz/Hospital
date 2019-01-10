﻿using System.Collections.Generic;
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
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public TreatmentBusiness(IMapper mapper,
            ITreatmentRepository treatmentRepository,
            IEmployeeRepository employeeRepository,
            IPatientRepository patientRepository,
            IMedicalRecordRepository medicalRecordRepository)
        {
            _mapper = mapper;
            _treatmentRepository = treatmentRepository;
            _employeeRepository = employeeRepository;
            _patientRepository = patientRepository;
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<TreatmentDto> Add(TreatmentDto model)
        {
            model.Code = string.Empty;
            var entity = _treatmentRepository.Add(_mapper.Map<TTreatment>(model));
            await _treatmentRepository.SaveChangeAsync();
            var maxId = await _medicalRecordRepository.Repo.MaxAsync(c => c.Id);
            entity.Code = $"BN-{(maxId + 1):D10}";
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

        public Task<IPaginatedList<TreatmentDto>> GetByMedicalRecordId(int medicalRecordId, int pageIndex, int pageSize)
        {
            var result = (from treatment in _treatmentRepository.Repo
                          .Where(c => c.MedicalRecordId == medicalRecordId && c.IsActived)
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

        public Task<TreatmentDto> GetById(int id)
        {
            var result = (from treatment in _treatmentRepository.Repo
                          .Where(c => c.Id == id && c.IsActived)
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
                          .FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(TreatmentDto model)
        {
            _treatmentRepository.Update(_mapper.Map<TTreatment>(model));
            var recordUpdated = await _treatmentRepository.SaveChangeAsync();
            return recordUpdated > 0;
        }

        public Task<IPaginatedList<TreatmentDto>> GetByPatientId(int patientId, int pageIndex, int pageSize)
        {
            var result = (from treatment in _treatmentRepository.Repo
                          .Where(c => c.PatientId == patientId && c.IsActived)
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

        public Task<IPaginatedList<TreatmentDto>> GetAll(int pageIndex, int pageSize)
        {

            var result = (from treatment in _treatmentRepository.Repo.Where(c => c.IsActived)
                          join patient in _patientRepository.Repo.Where(c => c.IsActived) on treatment.PatientId equals patient.Id
                          join medicalRecord in _medicalRecordRepository.Repo.Where(c => c.IsActived) on treatment.MedicalRecordId equals medicalRecord.Id
                          join doctor in _employeeRepository.Repo on treatment.DoctorId equals doctor.Id
                          into leftDoctors
                          from doctor in leftDoctors.DefaultIfEmpty()
                          join nurse in _employeeRepository.Repo on treatment.NurseId equals nurse.Id
                          into leftNurses
                          from nurse in leftNurses.DefaultIfEmpty()
                          select new TreatmentDto
                          {
                              Id = treatment.Id,
                              Code = treatment.Code,
                              PatientId = treatment.PatientId,
                              PatientFirstName = patient.FirstName,
                              PatientLastName = patient.LastName,
                              MedicalRecordId = treatment.MedicalRecordId,
                              MedicalRecordCode = medicalRecord.Code,
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
    }
}
