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
    public class TreatmentBusiness : ITreatmentBusiness
    {
        private readonly IMapper _mapper;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly ITreatmentDiseaseRepository _treatmentDiseaseRepository;
        private readonly IDiseaseRepository _diseaseRepository;

        public TreatmentBusiness(IMapper mapper,
            ITreatmentRepository treatmentRepository,
            IEmployeeRepository employeeRepository,
            IPatientRepository patientRepository,
            IMedicalRecordRepository medicalRecordRepository,
            ITreatmentDiseaseRepository treatmentDiseaseRepository,
            IDiseaseRepository diseaseRepository)
        {
            _mapper = mapper;
            _treatmentRepository = treatmentRepository;
            _employeeRepository = employeeRepository;
            _patientRepository = patientRepository;
            _medicalRecordRepository = medicalRecordRepository;
            _treatmentDiseaseRepository = treatmentDiseaseRepository;
            _diseaseRepository = diseaseRepository;
        }

        public async Task<TreatmentDetailDto> Add(TreatmentDetailDto model)
        {
            var entity = _mapper.Map<TTreatment>(model);
            entity.Code = string.Empty;
            if (model.Diseases != null && model.Diseases.Any())
            {
                entity.TTreatmentDisease = new List<TTreatmentDisease>();
                foreach (var disease in model.Diseases)
                {
                    entity.TTreatmentDisease.Add(new TTreatmentDisease
                    {
                        DiseaseId = disease.Id,
                        CreatedBy = model.CreatedBy,
                        CreatedTime = model.CreatedTime,
                        UpdatedBy = model.UpdatedBy,
                        UpdatedTime = model.UpdatedTime,
                        IsActived = true,
                    });
                }
            }
            _treatmentRepository.Add(entity);
            await _treatmentRepository.SaveChangeAsync();
            var maxId = await _treatmentRepository.Repo.MaxAsync(c => c.Id);
            entity.Code = $"DT-{(maxId + 1):D10}";
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

        public async Task<TreatmentDetailDto> GetById(int id)
        {
            var treatmentTask = (from treatment in _treatmentRepository.Repo.Where(c => c.Id == id && c.IsActived)
                                 join doctor in _employeeRepository.Repo on treatment.DoctorId equals doctor.Id
                                 into leftDoctors
                                 from doctor in leftDoctors.DefaultIfEmpty()
                                 join nurse in _employeeRepository.Repo on treatment.NurseId equals nurse.Id
                                 into leftNurses
                                 from nurse in leftNurses.DefaultIfEmpty()
                                 select new TreatmentDetailDto
                                 {
                                     Id = treatment.Id,
                                     Code = treatment.Code,
                                     PatientId = treatment.PatientId,
                                     MedicalRecordId = treatment.MedicalRecordId,
                                     StartDate = treatment.StartDate,
                                     EndDate = treatment.EndDate,
                                     DoctorId = treatment.DoctorId,
                                     NurseId = treatment.NurseId,
                                     Title = treatment.Title,
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

            var treatmentDiseaseIdsTask = (from treatmentDisease in _treatmentDiseaseRepository.Repo.Where(c => c.TreatmentId == id && c.IsActived)
                                           join disease in _diseaseRepository.Repo.Where(c => c.IsActived) on treatmentDisease.DiseaseId equals disease.Id
                                           select new DiseaseDto
                                           {
                                               Id = disease.Id,
                                               Name = disease.Name,
                                               Symptoms = disease.Symptoms,
                                               Treatment = disease.Treatment,
                                               Description = disease.Description
                                           })
                                          .ToListAsync();

            await Task.WhenAll(treatmentTask, treatmentDiseaseIdsTask);

            if (treatmentTask.Result != null)
            {
                treatmentTask.Result.Diseases = treatmentDiseaseIdsTask.Result;
            }

            return treatmentTask.Result;
        }

        public async Task<bool> Update(TreatmentDetailDto model)
        {
            var entity = await _treatmentRepository.Repo
                .Include(c => c.TTreatmentDisease)
                .FirstOrDefaultAsync(c => c.Id == model.Id);
            entity.PatientId = model.PatientId;
            entity.MedicalRecordId = model.MedicalRecordId;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.DoctorId = model.DoctorId;
            entity.NurseId = model.NurseId;
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.UpdatedBy = model.UpdatedBy;
            entity.UpdatedTime = model.UpdatedTime;
            entity.Note = model.Note;

            var insertedDiseases = model.Diseases.Where(c => !entity.TTreatmentDisease.Any(x => x.DiseaseId == c.Id));
            foreach (var newDisease in insertedDiseases)
            {
                entity.TTreatmentDisease.Add(new TTreatmentDisease
                {
                    DiseaseId = newDisease.Id,
                    CreatedTime = entity.UpdatedTime,
                    CreatedBy = entity.UpdatedBy,
                    UpdatedTime = entity.UpdatedTime,
                    UpdatedBy = entity.UpdatedBy,
                    IsActived = true,
                    IsDeleted = false
                });
            }

            var removedDiseases = entity.TTreatmentDisease.Where(c => !model.Diseases.Any(x => x.Id == c.DiseaseId));
            foreach (var removedDisease in removedDiseases)
            {
                removedDisease.UpdatedBy = entity.UpdatedBy;
                removedDisease.UpdatedTime = entity.UpdatedTime;
                removedDisease.IsActived = false;
            }

            var keepDiseases = entity.TTreatmentDisease.Where(c => model.Diseases.Any(x => x.Id == c.DiseaseId));
            foreach (var removedDisease in removedDiseases)
            {
                removedDisease.UpdatedBy = entity.UpdatedBy;
                removedDisease.UpdatedTime = entity.UpdatedTime;
                removedDisease.IsActived = true;
            }

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
                              Title = treatment.Title,
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
