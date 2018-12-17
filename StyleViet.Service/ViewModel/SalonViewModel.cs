using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StyleViet.Service.ViewModel
{
    public class SalonViewModel : AccountViewModel
    {
        public int SalonId { get; set; }
        [Required]
        [Display(Name = "Salon Name")]
        public string Name { get; set; } 
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public IEnumerable<SalonServiceViewModel> Services { get; set; }
    }
}
