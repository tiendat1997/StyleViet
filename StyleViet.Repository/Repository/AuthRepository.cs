using Microsoft.EntityFrameworkCore;
using StyleViet.Entity;
using StyleViet.Repository.Context;
using StyleViet.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace StyleViet.Repository.Repository
{
    public class AuthRepository : Repository<Account>, IAuthRepository
    {
        public AuthRepository(StyleVietContext context) : base(context)
        {
        }
        private string GetConnectionString()
        {
            return _context.Database.GetDbConnection().ConnectionString;
        }

        public string RegisterAdminAccount(Account account, Admin admin)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("RegisterAdminAccount", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", account.Username);
                cmd.Parameters.AddWithValue("@password", account.Password);
                cmd.Parameters.AddWithValue("@email", account.Email);
                cmd.Parameters.AddWithValue("@fullname", admin.Fullname);
                cmd.Parameters.AddWithValue("@phone", admin.Phone);
                con.Open();
                string result = cmd.ExecuteScalar().ToString();
                con.Close();
                return result;
            }
        }


        public string GetAccountPassword(string hashedUsername, int roleId)
        {
            var foundedAccount = _context.Account
                        .Where(a => a.Username.Equals(hashedUsername) && a.AccountRoles.Any(r => r.RoleId == roleId))
                        .FirstOrDefault();

            if (foundedAccount != null)
            {
                return foundedAccount.Password;
            }
            return null;
        }
    }
}
