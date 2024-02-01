using DTO.Interface;
using InoviDataTransferObject.IRepo;

namespace DTO
{
    public interface IRepositoryWrapper
    {
        public IUserRepo UserRepo { get; }
        public IQueryRepo QueryRepo { get; }
    }
}
