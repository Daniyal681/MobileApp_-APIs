using System;
using System.Collections.Generic;

namespace InoviDataAccessLayer.EntityModel
{
    public partial class TblQueryAttachment
    {
        public int QueryAttachmentId { get; set; }
        public int? QueryId { get; set; }
        public int? AttachmentLinkId { get; set; }
        public bool? IsActive { get; set; }

        public virtual TblAttachment? AttachmentLink { get; set; }
        public virtual TblQuery? Query { get; set; }
    }
}
