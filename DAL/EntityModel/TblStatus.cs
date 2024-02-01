using System;
using System.Collections.Generic;

namespace InoviDataAccessLayer.EntityModel
{
    public partial class TblStatus
    {
        public TblStatus()
        {
            TblQueryStatuses = new HashSet<TblQueryStatus>();
        }

        public int StatusId { get; set; }
        public string? Status { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TblQueryStatus> TblQueryStatuses { get; set; }
    }
}
