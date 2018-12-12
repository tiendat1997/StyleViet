using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StyleViet.Service.ViewModel
{
    public class AdminViewModel : AccountViewModel
    {
        public int AdminId { get; set; }
        [Required]
        public string Fullname { get; set; }        
        public string Phone { get; set; }
    }
}
