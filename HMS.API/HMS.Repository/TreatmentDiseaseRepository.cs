using HMS.Entities.Models;
using HMS.Repository.Interfaces;

namespace HMS.Repository
{
    public class TreatmentDiseaseRepository : BaseRepository<TTreatmentDisease>, ITreatmentDiseaseRepository
    {
        public TreatmentDiseaseRepository(HealthContext context)
            : base(context)
        {
        }
    }
}
