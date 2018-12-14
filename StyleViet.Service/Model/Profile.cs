﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Service.Model
{
    public class Profile
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }        
        public int RoleId { get; set; }
        public string GoogleId { get; set; }
        public string FacebookId { get; set; }
    }
}
