using StyleViet.Entity;
using StyleViet.Repository.Interface;
using StyleViet.Service.Enum;
using StyleViet.Service.Helper;
using StyleViet.Service.Interface;
using StyleViet.Service.Model;
using StyleViet.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    return new LoginResultViewModel { IsSuccess = true, RoleId = roleId, Username = hashedUsername };
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
        private Account RegisterAccount(string username, string password, string email, string googleId=null, string facebookId=null)
        {
            Tuple<string, string> hashing = new Tuple<string, string>(null,null);
            if (!string.IsNullOrWhiteSpace(password))
            {
                hashing = HashingHelper.GenerateHash(password);
            }
            
            Account account = new Account
            {
                Username = HashingHelper.GenerateSHA256Hash(username),
                Password = hashing.Item1,
                Salt = hashing.Item2,
                Email = email,
                GoogleId = googleId,
                FacebookId = facebookId,
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

        public Task CreateAsync(Profile model)
        {
            string result = "";
            var account = this.RegisterAccount(model.UserName, null, model.Email, model.GoogleId, model.FacebookId);
            if (model.RoleId == (int)RoleEnum.Salon)
            {
                Salon salon = new Salon
                {
                    Name = model.Name,
                    Address = model.Address,
                    Phone = model.Phone,
                    Email = model.Email
                };
                result = _authRepo.RegisterSalonAccount(account, salon);
            }
            else
            {
                User member = new User
                {
                    FirstName = model.Name,                    
                    Email = model.Email,
                    Phone = model.Phone
                };
                result = _authRepo.RegisterMemberAccount(account, member);
            }
            return Task.CompletedTask; 
        }

        public Task<Profile> RetrieveAsync(string username)
        {
            var hashedUsername = HashingHelper.GenerateSHA256Hash(username);
            var foundedAcc = _authRepo.GetAccountPassword(hashedUsername);
            if (foundedAcc != null)
            {
                var profile = new Profile
                {
                    UserName = username,                       
                    Email = foundedAcc.Email,
                    RoleId = foundedAcc.AccountRoles.Select(r => r.RoleId).FirstOrDefault()
                };
                return Task.FromResult<Profile>(profile);
            }
            return Task.FromResult<Profile>(null);
        }
    }
}
