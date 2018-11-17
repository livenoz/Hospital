using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using HMS.Business.Interfaces;
using HMS.Common.Dtos;
using HMS.Entities.Models;
using HMS.Repository.Interfaces;

namespace HMS.Business
{
    public class PatientBusiness : IPatientBusiness
    {
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;
        public PatientBusiness(IMapper mapper, IPatientRepository patientRepository)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
        }

        public Task<PatientDto> Add(PatientDto model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<PatientDto>> GetAll()
        {
            var result = await _patientRepository.GetAllAsync();
            return result.Select(_mapper.Map<PatientDto>).ToList();
        }

        public Task<PatientDto> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(PatientDto model)
        {
            throw new System.NotImplementedException();
        }
    }
}
