    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StyleViet.Entity
{
    public class Account : BaseEntity
    {     
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; } 
        public string Password { get; set; }
        public string Email { get; set; }        
        public bool Expired { get; set; }
        public string GoogleId { get; set; }
        public string FacebookId { get; set; }        
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
