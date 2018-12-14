using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace StyleViet.Service.ViewModel
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Email address is not in valid format.")]
        [Required(ErrorMessage = "Email address is required.")]
        public string Email { get; set; }

        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string GoogleId { get; set; }
        public string FacebookId { get; set; }
        public string ReturnUrl { get; set; }
    }
}
