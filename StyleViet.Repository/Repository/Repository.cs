using Microsoft.EntityFrameworkCore;
using StyleViet.Entity;
using StyleViet.Repository.Context;
using StyleViet.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StyleViet.Repository.Repository
{
    public class Repository<DbContext, T> : IRepository<DbContext, T> where T : BaseEntity
    {
        protected readonly StyleVietContext _context;

        public Repository(StyleVietContext context) => _context = context;
        

        public int Count(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public void Create(T entity)
        {
            _context.Add(entity);       
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);        
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;            
        }

        protected void Save() => _context.SaveChanges();
    }
}
