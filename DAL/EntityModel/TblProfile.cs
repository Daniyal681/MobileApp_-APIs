using System;
using System.Collections.Generic;

namespace InoviDataAccessLayer.EntityModel
{
    public partial class TblProfile
    {
        public int ProfileId { get; set; }
        public string? ProfilePath { get; set; }
        public string? ProfileLink { get; set; }
        public int? UserId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public byte[]? Profilebyte { get; set; }

        public virtual TblUser? User { get; set; }
    }
}
