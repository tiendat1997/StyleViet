using StyleViet.Service.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StyleViet.Service.Interface
{
    public interface IProfileService
    {
        Task CreateAsync(Profile profile);
        Task<Profile> RetrieveAsync(string username);
    }
}
