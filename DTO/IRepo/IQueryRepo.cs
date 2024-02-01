using InoviDataTransferObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InoviDataTransferObject.IRepo
{
    public interface IQueryRepo
    {
        Task<List<GetQueryDTO>> GetQueryList(GetUserIDAndRoleID req);
        Task<GetQueryByIdDTO> GetQueryById(int QueryId);
        Task<bool> AddQuery(AddQueryDTO req);
        Task<bool> UpdateQuery(UpdateStatus req);
        Task <AttachmentLinkList> UploadImage(AttachmentLinkList req);
    }
}
