using StyleViet.Service.Interface;
using StyleViet.Service.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StyleViet.Service.Implement
{
    public class ProfileService : IProfileService
    {
        private IDictionary<string, Profile> _profiles = new Dictionary<string, Profile>();
        public Task CreateAsync(Profile profile)
        {
            _profiles.Add(profile.UserName, profile);
            return Task.CompletedTask;
        }

        public Task<Profile> RetrieveAsync(string username)
        {
            if (_profiles.TryGetValue(username, out Profile profile))
            {
                return Task.FromResult(profile);
            }

            return Task.FromResult<Profile>(null);
        }
    }
}
