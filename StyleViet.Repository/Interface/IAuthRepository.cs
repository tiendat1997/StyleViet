using StyleViet.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Repository.Interface
{
    public interface IAuthRepository : IRepository<Account>
    {       
        string GetAccountPassword(string hashedUsername,int roleId);
        Account GetAccountPassword(string hashedUsername);
        string RegisterAdminAccount(Account account, Admin admin);
        string RegisterSalonAccount(Account account, Salon salon);
        string RegisterMemberAccount(Account account, User member);
    }
}
