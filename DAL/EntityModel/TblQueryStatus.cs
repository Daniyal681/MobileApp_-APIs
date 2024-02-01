using System;
using System.Collections.Generic;

namespace InoviDataAccessLayer.EntityModel
{
    public partial class TblQueryStatus
    {
        public int Id { get; set; }
        public int? QueryId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblQuery? Query { get; set; }
        public virtual TblStatus? Status { get; set; }
    }
}
