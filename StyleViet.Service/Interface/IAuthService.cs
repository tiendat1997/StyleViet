using StyleViet.Service.Model;
using StyleViet.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StyleViet.Service.Interface
{
    public interface IAuthService 
    {        
        string RegisterAdmin(AdminViewModel model);
        string RegisterMember(MemberViewModel model);
        string RegisterSalon(SalonViewModel model);
        string AdminLogin(LoginViewModel model);
        LoginResultViewModel AccountLogin(LoginViewModel model);

        #region External Login
        Task CreateAsync(Profile profile);
        Task<Profile> RetrieveAsync(string username);
    }
    #endregion
}
