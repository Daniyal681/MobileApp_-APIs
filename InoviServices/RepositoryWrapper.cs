using DTO;
using DTO.Interface;
using InoviDataAccessLayer.EntityModel;
using InoviDataTransferObject.IRepo;
using InoviServices.Repo;
using Microsoft.Extensions.Configuration;


namespace InoviServices
{
    public class RepositoryWrapper :IRepositoryWrapper
    {
        private QueryProContext _context;
        private IUserRepo _userRepo;
        private IQueryRepo _queryRepo;
        public RepositoryWrapper(QueryProContext context)
        {
            _context = context;
        }

        public IUserRepo UserRepo
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new UserRepo(_context);
                }
                return _userRepo;
            }
        }

        public IQueryRepo QueryRepo 
        {
            get
            {
                if (_queryRepo == null)
                {
                    _queryRepo = new QueryRepo(_context);
                }
                return _queryRepo;
            }
        }
    }
}
