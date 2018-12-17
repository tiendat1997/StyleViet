using StyleViet.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StyleViet.Service.Interface
{
    public interface ISalonService
    {
        SalonViewModel GetProfile(string username);
        void UpdateProfile(SalonViewModel model);
        Task<IEnumerable<SalonViewModel>> GetAllSalon();

    }
}
