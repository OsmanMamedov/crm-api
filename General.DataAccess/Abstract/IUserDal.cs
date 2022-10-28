using General.Core.DataAccess;
using General.Core.Entities;
using General.Entities;
using General.Entities.Complex.Dtos;
using System.Collections.Generic;

namespace General.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        IEnumerable<UserDto> GetUsers(Request request, out int count);
        User GetByPhone(string phone);
        bool Exists(string phone);
    }
}
