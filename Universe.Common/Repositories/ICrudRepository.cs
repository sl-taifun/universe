using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Universe.Common.Repositories
{
    public interface ICrudRepository<TId,TModel>
    {
        // Create
        TModel Create(TModel model);

        // Read
        TModel? GetById(TId id);
        IEnumerable<TModel> GetAll();
        // update
       bool Update(TId id,TModel model);
        // Delete
       bool Delete(TId id);
    }
}
