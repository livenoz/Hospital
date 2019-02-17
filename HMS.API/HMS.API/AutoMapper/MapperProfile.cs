using AutoMapper;
using HMS.Entities.Models;
using HMS.Common.Dtos.Patient;

namespace HMS.API.AutoMapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<TPatient, PatientDto>();
            CreateMap<PatientDto, TPatient>();

            CreateMap<TMedicalRecord, MedicalRecordDto>();
            CreateMap<MedicalRecordDto, TMedicalRecord>();

            CreateMap<TTreatment, TreatmentDto>();
            CreateMap<TreatmentDto, TTreatment>();

            CreateMap<TTreatment, TreatmentDiseaseDto>();
            CreateMap<TreatmentDiseaseDto, TTreatment>();

            CreateMap<TPrescription, PrescriptionDto>();
            CreateMap<PrescriptionDto, TPrescription>();

            CreateMap<TDisease, DiseaseDto>();
            CreateMap<DiseaseDto, TDisease>();

            CreateMap<TTreatmentDetail, TreatmentDetailDto>();
            CreateMap<TreatmentDetailDto, TTreatmentDetail>();
        }
    }
}
