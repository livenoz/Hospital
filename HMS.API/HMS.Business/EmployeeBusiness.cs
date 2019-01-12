using AutoMapper;
using HMS.Business.Interfaces;
using HMS.Business.Interfaces.Paginated;
using HMS.Business.Paginated;
using HMS.Common.Dtos.Employee;
using HMS.Common.Dtos.Patient;
using HMS.Common.Enums;
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
    public class EmployeeBusiness : IEmployeeBusiness
    {

        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeBusiness(IMapper mapper,
            IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public Task<IPaginatedList<DoctorDto>> GetDoctors(int pageIndex, int pageSize)
        {
            var result = _employeeRepository.Repo.Where(c => c.TypeId == (int)EEmployeeType.Doctor && c.IsActived)
                .Select(c => new DoctorDto
                {
                    Id = c.Id,
                    Code = c.Code,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                })
                .OrderByDescending(c => c.Id)
                .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }

        public Task<IPaginatedList<NurseDto>> GetNurses(int pageIndex, int pageSize)
        {
            var result = _employeeRepository.Repo.Where(c => c.TypeId == (int)EEmployeeType.Nurse && c.IsActived)
                .Select(c => new NurseDto
                {
                    Id = c.Id,
                    Code = c.Code,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                })
                .OrderByDescending(c => c.Id)
                .ToPaginatedListAsync(pageIndex, pageSize);
            return result;
        }
    }
}
