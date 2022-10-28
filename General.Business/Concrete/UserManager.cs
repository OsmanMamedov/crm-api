using General.Business.Abstract;
using General.Core.Entities;
using General.Core.Entities.Concrete;
using General.DataAccess.Abstract;
using General.Entities;
using General.Entities.Complex.Dtos;
using System.Linq;

namespace General.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _UserDal;
        private readonly IUserRoleDal _UserRoleDal;

        public UserManager()
        {

        }

        public UserManager(IUserDal UserDal,
                           IUserRoleDal UserRoleDal)
        {
            _UserRoleDal = UserRoleDal;
            _UserDal = UserDal;
        }

        public ResponseService Add(UserDto item)
        {
            ResponseService ResponseService = new ResponseService
            {
                Success = true,
                Data = _UserDal.Add(item.GetUser()),
            };
            return ResponseService;
        }

        public ResponseService AddRole(UserRole item)
        {
            ResponseService ResponseService = new ResponseService
            {
                Success = true,
                Data = _UserRoleDal.Add(item)
            };
            return ResponseService;
        }


        public ResponseService Any(UserDto item)
        {
            ResponseService ResponseService = new ResponseService
            {
                Data = _UserDal.Any(item.GetUser()),
                Success = true
            };
            return ResponseService;
        }

        public ResponseService Delete(UserDto item)
        {
            ResponseService ResponseService = new ResponseService
            {
                Success = _UserDal.Delete(item.GetUser())
            };
            return ResponseService;
        }

        public ResponseService DeleteRole(UserRole item)
        {
            ResponseService ResponseService = new ResponseService
            {
                Success = _UserRoleDal.Delete(item)
            };
            return ResponseService;
        }

        public bool Exists(string phone)
        {
            return _UserDal.Exists(phone);
        }

        public UserDto Get(Request request)
        {
            return new UserDto(_UserDal.Get(request));
        }

        public UserDto GetByPhone(string phone)
        {
            return new UserDto(_UserDal.GetByPhone(phone));
        }

        public ResponseService GetList(Request request)
        {
            ResponseService responseService = new ResponseService
            {
                Data = _UserDal.GetUsers(request, out int count),
                Count = count,
                Success = true
            };
            return responseService;
        }

        public ResponseService Update(UserDto item)
        {
            ResponseService ResponseService = new ResponseService
            {
                Data = new UserDto(_UserDal.Update(item.GetUser())),
                Success = true
            };
            return ResponseService;
        }
    }
}
