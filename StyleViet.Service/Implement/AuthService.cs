using StyleViet.Entity;
using StyleViet.Repository.Interface;
using StyleViet.Service.Enum;
using StyleViet.Service.Helper;
using StyleViet.Service.Interface;
using StyleViet.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public LoginResultViewModel AccountLogin(LoginViewModel model)
        {
            var hashedUsername = HashingHelper.GenerateSHA256Hash(model.Username);
            var foundedAcc = _authRepo.GetAccountPassword(hashedUsername);
            if (foundedAcc != null)
            {
                var pass = foundedAcc.Password;
                var salt = pass.Substring(pass.Length - 7);
                pass = pass.Remove(pass.Length - 7);
                var hashed = HashingHelper.ValidateHash(model.Password, salt);
                int roleId = foundedAcc.AccountRoles.Select(r => r.RoleId).FirstOrDefault();
                if (hashed.Equals(pass)) return new LoginResultViewModel { IsSuccess = true, RoleId = roleId };
            }
            return new LoginResultViewModel { IsSuccess = false };
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

        public string RegisterMember(MemberViewModel model)
        {
            Account account = new Account
            {
                Username = HashingHelper.GenerateSHA256Hash(model.Username),
                Password = HashingHelper.GenerateHash(model.Password),
                Email = model.Email,
                Expired = false,
            };
            User member = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone
            };
            return _authRepo.RegisterMemberAccount(account, member);
        }

        public string RegisterSalon(SalonViewModel model)
        {
            Account account = new Account
            {
                Username = HashingHelper.GenerateSHA256Hash(model.Username),
                Password = HashingHelper.GenerateHash(model.Password),
                Email = model.Email,
                Expired = false,
            };
            Salon salon = new Salon
            {
                Name = model.Name,
                Address = model.Address,
                Phone = model.Phone,
                Email = model.Email
            };
            return _authRepo.RegisterSalonAccount(account, salon);
        }
    }
}
