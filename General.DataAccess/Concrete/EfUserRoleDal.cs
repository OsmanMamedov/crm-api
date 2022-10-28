using General.Core.DataAccess.EntityFramework;
using General.DataAccess.Abstract;
using General.Entities;

namespace General.DataAccess.Concrete
{
    public class EfUserRoleDal : EfBaseEntityRepository<UserRole, GeneralContext>, IUserRoleDal
    {

    }
}
