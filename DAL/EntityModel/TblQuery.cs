using System;
using System.Collections.Generic;

namespace InoviDataAccessLayer.EntityModel
{
    public partial class TblQuery
    {
        public TblQuery()
        {
            TblQueryAttachments = new HashSet<TblQueryAttachment>();
            TblQueryStatuses = new HashSet<TblQueryStatus>();
        }

        public int QueryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public string? CurrentStatus { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? RemarksOn { get; set; }
        public int? RemarksBy { get; set; }

        public virtual TblUser? User { get; set; }
        public virtual ICollection<TblQueryAttachment> TblQueryAttachments { get; set; }
        public virtual ICollection<TblQueryStatus> TblQueryStatuses { get; set; }
    }
}
