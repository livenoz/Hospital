﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HMS.Entities;
using HMS.Entities.Models;
using HMS.Business.Interfaces;

namespace HMS.API.Controllers.V1
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UsersController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<TUser> GetUsers()
        {
            return _userBusiness.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
