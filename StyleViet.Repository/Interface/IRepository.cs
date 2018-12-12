using StyleViet.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Repository.Interface
{
    public interface IRepository<T> where T: BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
        T FindById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Count(Func<T, bool> predicate);
    }
}
