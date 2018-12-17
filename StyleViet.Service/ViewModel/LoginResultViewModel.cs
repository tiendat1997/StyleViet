using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Service.ViewModel
{
    public class LoginResultViewModel
    {
        public bool IsSuccess { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
    }
}
