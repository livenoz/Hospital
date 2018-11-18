using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using HMS.Business.Interfaces;
using HMS.Common.Dtos;
using HMS.Entities.Models;
using HMS.Repository.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Business.Paginated;

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

        public async Task<PatientDto> Add(PatientDto model)
        {
            var entity = _patientRepository.Add(_mapper.Map<TPatient>(model));
            await _patientRepository.SaveChangeAsync();
            model.Id = entity.Id;
            return model;
        }

        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IPaginatedList<PatientDto>> GetAll(int pageIndex = 0, int pageSize = 20)
        {
            var result = await _patientRepository.Repo
                .Select(c => new PatientDto
                {
                    Id = c.Id
                }).ToPaginatedListAsync(pageIndex, pageSize);
            return result;
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
