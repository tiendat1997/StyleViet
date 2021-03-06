﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StyleViet.Entity
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
