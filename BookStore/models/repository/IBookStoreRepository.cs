using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.models.repository
{
  public  interface IBookStoreRepository<TEntity>
    {
        IList<TEntity> list();

        TEntity find(int id);

        void add(TEntity t);

        void update(int id,TEntity t);

        void delete(int id);
    }
}
