using StyleViet.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Repository.Interface
{
    public interface IAuthRepository : IRepository<Account>
    {       
        string GetAccountPassword(string hashedUsername,int roleId);
        string RegisterAdminAccount(Account account, Admin admin);    
    }
}
