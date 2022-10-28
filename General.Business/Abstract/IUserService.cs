using General.Core.Entities;
using General.Core.Entities.Concrete;
using General.Entities;
using General.Entities.Complex.Dtos;

namespace General.Business.Abstract
{
    public interface IUserService
    {
        ResponseService GetList(Request request);
        UserDto Get(Request request);
        ResponseService Any(UserDto item);
        ResponseService Add(UserDto item);
        ResponseService Update(UserDto item);
        ResponseService Delete(UserDto item);
        UserDto GetByPhone(string phone);
        bool Exists(string phone);
        ResponseService AddRole(UserRole item);
        ResponseService DeleteRole(UserRole item);
    }
}
