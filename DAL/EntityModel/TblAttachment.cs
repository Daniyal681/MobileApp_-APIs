using System;
using System.Collections.Generic;

namespace InoviDataAccessLayer.EntityModel
{
    public partial class TblAttachment
    {
        public TblAttachment()
        {
            TblQueryAttachments = new HashSet<TblQueryAttachment>();
        }

        public int AttachmentLinkId { get; set; }
        public string? Path { get; set; }
        public string? AttachmentLink { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }
        public byte[] Filebytes { get; set; } = null!;

        public virtual ICollection<TblQueryAttachment> TblQueryAttachments { get; set; }
    }
}
