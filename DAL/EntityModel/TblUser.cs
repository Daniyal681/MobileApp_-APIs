using System;
using System.Collections.Generic;

namespace InoviDataAccessLayer.EntityModel
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblProfiles = new HashSet<TblProfile>();
            TblQueries = new HashSet<TblQuery>();
        }

        public int UserId { get; set; }
        public int? UserRoleId { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblRole? UserRole { get; set; }
        public virtual ICollection<TblProfile> TblProfiles { get; set; }
        public virtual ICollection<TblQuery> TblQueries { get; set; }
    }
}
