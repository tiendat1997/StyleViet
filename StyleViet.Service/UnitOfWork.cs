using StyleViet.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StyleVietContext _context;
        public UnitOfWork(StyleVietContext dbContext) => _context = dbContext; 
        
        public void Save() => _context.SaveChanges();
        
    }
}
