using System;
using System.Collections.Generic;

namespace InoviDataAccessLayer.EntityModel
{
    public partial class TblRole
    {
        public TblRole()
        {
            TblUsers = new HashSet<TblUser>();
        }

        public int UserRoleId { get; set; }
        public string? Role { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
