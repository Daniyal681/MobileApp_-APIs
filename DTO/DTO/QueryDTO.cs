using DTO.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InoviDataTransferObject.DTO
{
    public class GetQueryDTO
    {
        public int QueryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CurrentStatus { get; set; }
        public List<string> AttachmentPaths { get; set; }

        public string Remarks { get; set; }
    }

    public class GetQueryByIdDTO
    {
        public int QueryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CurrentStatus { get; set; }
        public List<string> AttachmentPaths { get; set; }
        public string Remarks { get; set; }
    }

    public class AddQueryDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<AttachmentLinkList> AttachmentIds { get; set; }
        public List<string> Attachments { get; set; }
        public int UserID { get; set; }
    }

    public class AttachmentLinkList
    {
        public int AttachmentLinkId { get; set; }
        public string FileName { get; set; }
        public string AttachmentLink { get; set; }
        public string Path { get; set; }
        public byte[] FileBytes { get; set; }
        public bool IsActive { get; } = true;
        public int UserID { get; set; }

    }
   public class GetUserIDAndRoleID
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
    public class UpdateStatus
    {
        public int QueryID { get; set; }
        public EStatus StatusID { get; set; }
        public string? Status { get { return StatusID.ToString(); } }
        public int UserID { get; set; }
        public string Remarks { get; set; }
    }
}
