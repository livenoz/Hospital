using AutoMapper;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Business.Paginated;
using HMS.Common.Dtos.Employee;
using HMS.Common.Dtos.Patient;
using HMS.Entities.Models;
using HMS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Business
{
    public class DiseaseBusiness : IDiseaseBusiness
    {

        private readonly IMapper _mapper;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IDiseaseGroupRepository _diseaseGroupRepository;

        public DiseaseBusiness(IMapper mapper,
            IDiseaseRepository diseaseRepository,
            IDiseaseGroupRepository diseaseGroupRepository)
        {
            _mapper = mapper;
            _diseaseRepository = diseaseRepository;
            _diseaseGroupRepository = diseaseGroupRepository;
        }

        public Task<IPaginatedList<DiseaseDto>> GetAll(int pageIndex, int pageSize)
        {
            return _diseaseRepository.Repo
                .Select(c => new DiseaseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Symptoms = c.Symptoms,
                    Treatment = c.Treatment,
                    Description = c.Description
                })
                .OrderBy(c => c.Name)
                .ToPaginatedListAsync(pageIndex, pageSize);
        }


    }
}
