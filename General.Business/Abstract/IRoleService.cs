using General.Core.Entities;
using General.Core.Entities.Concrete;
using General.Entities;
using General.Entities.Complex.Dtos;

namespace General.Business.Abstract
{
    public interface IRoleService
    {
        ResponseService GetList(Request request);
        ResponseService Get(Request request);
        ResponseService Any(RoleDto item);
        ResponseService Add(RoleDto item);
        ResponseService Update(RoleDto item);
        ResponseService Delete(RoleDto item);
    }
}
