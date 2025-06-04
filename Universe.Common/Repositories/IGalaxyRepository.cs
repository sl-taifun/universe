using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Common.Models;

namespace Universe.Common.Repositories
{
    public interface IGalaxyRepository : ICrudRepository<int, Galaxy>
    {
        bool AddStar(int galaxyId, Star star);
    }
}
