using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace StyleViet.Service.ViewModel
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
