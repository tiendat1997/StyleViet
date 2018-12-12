using StyleViet.Entity;
using StyleViet.Repository.Interface;
using StyleViet.Service.Enum;
using StyleViet.Service.Helper;
using StyleViet.Service.Interface;
using StyleViet.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Service.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthRepository _authRepo;

        public AuthService(IUnitOfWork unitOfWork, IAuthRepository authRepo)
        {
            _unitOfWork = unitOfWork;
            _authRepo = authRepo;

        }
        public string AdminLogin(LoginViewModel model)
        {
            var hashedUsername = HashingHelper.GenerateSHA256Hash(model.Username);
            var pass = _authRepo.GetAccountPassword(hashedUsername, (int)RoleEnum.Admin);            
            if (pass != null)
            {
                var salt = pass.Substring(pass.Length - 7);
                pass = pass.Remove(pass.Length - 7);
                var hashed = HashingHelper.ValidateHash(model.Password, salt);
                if (hashed.Equals(pass)) return "Success";
            }            
            return "Fail";
        }
       
        public string RegisterAdmin(AdminViewModel model)
        {
            Account account = new Account
            {
                Username = HashingHelper.GenerateSHA256Hash(model.Username),
                Password = HashingHelper.GenerateHash(model.Password),
                Email = model.Email,
                Expired = false,               
            };
            Admin admin = new Admin
            {
                Fullname = model.Fullname,
                Phone = model.Phone
            };
            return _authRepo.RegisterAdminAccount(account, admin);
        }
    }
}
