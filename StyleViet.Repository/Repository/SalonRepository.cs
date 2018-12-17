using StyleViet.Entity;
using StyleViet.Repository.Context;
using StyleViet.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Repository.Repository
{
    public class SalonRepository : Repository<Salon>, ISalonRepository
    {
        public SalonRepository(StyleVietContext context) : base(context)
        {
        }
    }
}
