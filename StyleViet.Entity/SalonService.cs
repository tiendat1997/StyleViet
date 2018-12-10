using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StyleViet.Entity
{
    public class SalonService
    {        
        public int SalonId { get; set; }     
        public int ServiceId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Disabled { get; set; }

        public virtual Salon Salon { get; set; }
        public virtual Service Service { get; set; }
    }
}
