﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HMS.Business.Interfaces;
using HMS.Common.Dtos.User;
using HMS.Common.Enums;
using HMS.Common.Responses.User;
using HMS.Entities.Models;
using HMS.Repository.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HMS.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IMapper _mapper;
        private readonly IDataProtector _protector;
        private readonly IUserRepository _userRepository;

        public UserBusiness(IMapper mapper,
            IDataProtectionProvider provider,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _protector = provider.CreateProtector("Test");
            _userRepository = userRepository;
        }

        public TUser Add(TUser user)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckAuthentication(string accessToken)
        {
            var result = false;
            var model = JsonConvert.DeserializeObject<AuthenticationDto>(_protector.Unprotect(accessToken));
            if(model.UserId > 0)
            {
                result = true;
            }
            return result;
        }

        public bool Delete(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TUser> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public TUser GetById(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<LoginResponse> Login(LoginDto model)
        {
            var result = new LoginResponse
            {
                Status = EResponseStatus.Fail
            };

            if(!string.IsNullOrWhiteSpace(model.Username) && !string.IsNullOrWhiteSpace(model.Password))
            {
                var user = await _userRepository.Repo.FirstOrDefaultAsync(c => c.Username.Equals(model.Username));
                if (user != null && user.IsActived && user.Password.Equals(model.Password))
                {
                    var authentication = new AuthenticationDto
                    {
                        UserId = user.Id,
                        Username = user.Username,
                        CreatedTime = DateTime.UtcNow
                    };
                    var accessToken = _protector.Protect(JsonConvert.SerializeObject(authentication));
                    user.AccessToken = accessToken;
                    await _userRepository.SaveChangeAsync();
                    result.Status = EResponseStatus.Success;
                    result.AccessToken = accessToken;
                }
            }

            return result;
        }

        public bool Update(TUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}
