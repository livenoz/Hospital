﻿using System;
using System.Collections.Generic;
using System.Text;
using HMS.Entities;
using HMS.Entities.Models;
using HMS.Repository.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HMS.Repository
{
    public class DiseaseRepository : BaseRepository<TDisease>, IDiseaseRepository
    {
        public DiseaseRepository(HealthContext context)
            : base(context)
        {
        }
    }
}
