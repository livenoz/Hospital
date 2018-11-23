using AutoMapper;
using HMS.Entities.Models;
using HMS.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS.API.AutoMapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<TPatient, PatientDto>();
            CreateMap<PatientDto, TPatient>()
                .ForMember(des => des.Code, opt => opt.MapFrom(c => c.Code));
        }
    }
}
