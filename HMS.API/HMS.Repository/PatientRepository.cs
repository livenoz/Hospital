using System;
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
    public class PatientRepository : BaseRepository<TPatient>, IPatientRepository
    {

        public PatientRepository(HealthContext context)
            : base(context)
        {
        }

        public override void Delete(int id)
        {
            _context.Set<TPatient>().Remove(new TPatient { Id = id });
        }
    }
}
