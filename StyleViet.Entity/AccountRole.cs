using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Entity
{
    public class AccountRole
    {
        public int AccountId { get; set; }
        public int RoleId { get; set; }
        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }

    }
}
