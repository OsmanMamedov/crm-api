using General.Core.DataAccess.EntityFramework;
using General.Core.Entities;
using General.DataAccess.Abstract;
using General.Entities;
using System.Linq;

namespace General.DataAccess.Concrete
{
    public class EfRoleDal : EfEntityRepository<Role, GeneralContext>, IRoleDal
    {

    }
}
