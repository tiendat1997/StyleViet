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
                var result = HashingHelper.ValidateHash(foundedAcc.Password,model.Password, foundedAcc.Salt);                
                if (result)
                {
                    int roleId = foundedAcc.AccountRoles.Select(r => r.RoleId).FirstOrDefault();
                    return new LoginResultViewModel { IsSuccess = true, RoleId = roleId };
                }                    
            }
            return new LoginResultViewModel { IsSuccess = false };
        }
        public string AdminLogin(LoginViewModel model)
        {
            var hashedUsername = HashingHelper.GenerateSHA256Hash(model.Username);
            var foundedAcc = _authRepo.GetAccountPassword(hashedUsername);
            if (foundedAcc != null)
            {
                var result = HashingHelper.ValidateHash(foundedAcc.Password, model.Password, foundedAcc.Salt);
                if (result)
                {
                    var role = foundedAcc.AccountRoles
                        .Where(r => r.RoleId == (int)RoleEnum.Admin)
                        .Select(r => r.Role).FirstOrDefault();
                    if (role != null) return "Success";
                }                                
            }
            return "Fail";
        }
        private Account RegisterAccount(string username, string password, string email)
        {
            var hashing = HashingHelper.GenerateHash(password);
            Account account = new Account
            {
                Username = HashingHelper.GenerateSHA256Hash(username),
                Password = hashing.Item1,
                Salt = hashing.Item2,
                Email = email,
                Expired = false,
            };
            return account;
        }

        public string RegisterAdmin(AdminViewModel model)
        {
            var account = this.RegisterAccount(model.Username,model.Password,model.Email);
            Admin admin = new Admin
            {
                Fullname = model.Fullname,
                Phone = model.Phone
            };
            return _authRepo.RegisterAdminAccount(account, admin);
        }

        public string RegisterMember(MemberViewModel model)
        {
            var account = this.RegisterAccount(model.Username, model.Password, model.Email);
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
            var account = this.RegisterAccount(model.Username, model.Password, model.Email);
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
