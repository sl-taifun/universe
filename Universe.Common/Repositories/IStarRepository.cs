using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Common.Models;

namespace Universe.Common.Repositories
{
    public interface IStarRepository : ICrudRepository<int, Star>
    {
        bool AddPlanet(int starId, int planetId);
        bool AddPlanets(int starId, IEnumerable<int> planetIds);
        bool RemovePlanet(int starId, int planetId);
    }
}
