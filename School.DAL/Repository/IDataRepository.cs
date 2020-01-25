using System;
using System.Collections.Generic;
using System.Text;

namespace School.DAL.Repository
{
    public interface IDataRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllByClassId(long id);
        TEntity Get(long id);
        void Add(TEntity entity);
        void Update(long id, TEntity entity);
        void Delete(long id);
    }
}
