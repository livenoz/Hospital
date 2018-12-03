﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HMS.API.Attributes;
using HMS.Business.Interfaces.Paginated;
using HMS.Common.Dtos.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [BearerAuthorize]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // GET: api/Employee
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Employee
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public async Task<IPaginatedList<DoctorDto>> GetDoctors()
        {
            throw new NotImplementedException();
        }


        public async Task<IPaginatedList<DoctorDto>> GetNurses()
        {
            throw new NotImplementedException();
        }
    }
}
