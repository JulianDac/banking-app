///-----------------------------------------------------------------
///   Raji Rudhrakumar                    
///   Assignment-3 NWBA Web Application
///   Summer Semester 2020
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaApi.Models
{
    public interface IDataRepository<TEntity, TKey> where TEntity : class
    {
            IEnumerable<TEntity> GetAll();
            TEntity Get(TKey id);
            TKey Add(TEntity item);
            TKey Update(TKey id, TEntity item);
            TKey Delete(TKey id);
        
    }
}

