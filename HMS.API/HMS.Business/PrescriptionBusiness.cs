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
using AutoMapper.QueryableExtensions;

namespace HMS.Business
{
    public class PrescriptionBusiness : IPrescriptionBusiness
    {
        private readonly IMapper _mapper;
        private readonly IPrescriptionDetailRepository _prescriptionDetailRepository;
        private readonly IDrugRepository _drugRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly ITreatmentDetailRepository _treatmentDetailRepository;

        public PrescriptionBusiness(IMapper mapper,
            IPrescriptionDetailRepository prescriptionDetailRepository,
            IDrugRepository drugRepository,
            IUnitRepository unitRepository,
            ITreatmentDetailRepository treatmentDetailRepository)
        {
            _mapper = mapper;
            _prescriptionDetailRepository = prescriptionDetailRepository;
            _drugRepository = drugRepository;
            _unitRepository = unitRepository;
            _treatmentDetailRepository = treatmentDetailRepository;
        }

        public async Task<List<int>> Add(List<PrescriptionDetailDto> models)
        {
            var entities = models.Select(_mapper.Map<TPrescriptionDetail>).ToList();
            _prescriptionDetailRepository.AddRange(entities);
            await _prescriptionDetailRepository.SaveChangeAsync();
            var result = entities.Select(c => c.Id).ToList();
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IPaginatedList<PrescriptionDetailDto>> GetByTreatmentDetailId(int treatmentDetailId)
        {
            var result = (from prescriptionDetail in _prescriptionDetailRepository.Repo.Where(c => c.TreatmentDetailId == treatmentDetailId && c.IsActived)
                          join drug in _drugRepository.Repo on prescriptionDetail.DrugId equals drug.Id
                          join unit in _unitRepository.Repo on prescriptionDetail.UnitId equals unit.Id
                          select new PrescriptionDetailDto
                          {
                              Id = prescriptionDetail.Id,
                              PatientId = prescriptionDetail.PatientId,
                              MedicalRecordId = prescriptionDetail.MedicalRecordId,
                              TreatmentId = prescriptionDetail.TreatmentId,
                              TreatmentDetailId = prescriptionDetail.TreatmentDetailId,
                              DrugId = prescriptionDetail.DrugId,
                              Amount = prescriptionDetail.Amount,
                              UnitId = prescriptionDetail.UnitId,
                              UseId = prescriptionDetail.UseId,
                              AmountMorning = prescriptionDetail.AmountMorning,
                              AmountAfternoon = prescriptionDetail.AmountAfternoon,
                              AmountEvening = prescriptionDetail.AmountEvening,
                              Diagnose = prescriptionDetail.Diagnose,
                              Note = prescriptionDetail.Note,
                              CreatedTime = prescriptionDetail.CreatedTime,
                              CreatedBy = prescriptionDetail.CreatedBy,
                              UpdatedTime = prescriptionDetail.UpdatedTime,
                              UpdatedBy = prescriptionDetail.UpdatedBy,
                              IsActived = prescriptionDetail.IsActived,
                              DrugName = drug.Name,
                              UnitName = unit.Name
                          })
                          .OrderByDescending(c => c.Id)
                          .ToPaginatedListAsync(0, 1000);
            return result;
        }

        public Task<IPaginatedList<DrugDto>> GetDrugs(int pageIndex, int pageSize)
        {
            var result = (from drug in _drugRepository.Repo.Where(c => c.IsActive)
                          join unit in _unitRepository.Repo on drug.UnitId equals unit.Id
                          select new DrugDto
                          {
                              Id = drug.Id,
                              Name = drug.Name,
                              GroupId = drug.GroupId,
                              UnitId = drug.UnitId,
                              UseId = drug.UseId,
                              AmountMorning = drug.AmountMorning,
                              AmountAfternoon = drug.AmountAfternoon,
                              AmountEvening = drug.AmountEvening,
                              Description = drug.Description,
                              Note = drug.Note,
                              IsActive = drug.IsActive,
                              UnitName = unit.Name
                          })
                          .OrderByDescending(c => c.Name)
                          .ToPaginatedListAsync(0, 1000);
            return result;
        }

        public Task<IPaginatedList<UnitDto>> GetUnits(int pageIndex, int pageSize)
        {
            var result = _unitRepository.Repo.Where(c => c.IsActive)
                .Select(c => new UnitDto
                {
                    Id = c.Id,
                    Code = c.Code,
                    Name = c.Name,
                    IsActive = c.IsActive,
                })
                .OrderBy(c => c.Name)
                .ToPaginatedListAsync(0, 1000);
            return result;
        }

        public async Task<bool> Update(int treatmentDetailId, List<PrescriptionDetailDto> models)
        {
            var oldPrescriptionDetails = await _prescriptionDetailRepository.Repo
                .Where(c => c.TreatmentDetailId == treatmentDetailId)
                .ToListAsync();
            _prescriptionDetailRepository.DeleteRange(oldPrescriptionDetails);

            var treatmentDetail = await _treatmentDetailRepository.Repo
                .FirstOrDefaultAsync(c => c.Id == treatmentDetailId);
            if (treatmentDetail != null && models != null && models.Any())
            {
                foreach (var model in models)
                {
                    model.PatientId = treatmentDetail.PatientId;
                    model.MedicalRecordId = treatmentDetail.MedicalRecordId;
                    model.TreatmentId = treatmentDetail.TreatmentId;
                    model.TreatmentDetailId = treatmentDetail.Id;
                }
                var entities = models.Select(_mapper.Map<TPrescriptionDetail>).ToList();
                _prescriptionDetailRepository.AddRange(entities);
            }

            var effectedRecords = await _prescriptionDetailRepository.SaveChangeAsync();
            return effectedRecords > 0;
        }
    }
}
