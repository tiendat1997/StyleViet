using StyleViet.Entity;
using StyleViet.Repository.Context;
using StyleViet.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Repository.Repository
{
    public class ClientRepository : Repository<User>, IClientRepository
    {
        public ClientRepository(StyleVietContext context) : base(context)
        {
        }
    }
}
