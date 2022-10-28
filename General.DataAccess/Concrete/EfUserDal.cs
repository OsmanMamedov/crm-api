using General.Core.DataAccess.EntityFramework;
using General.Core.Entities;
using General.DataAccess.Abstract;
using General.Entities;
using General.Entities.Complex.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace General.DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepository<User, GeneralContext>, IUserDal
    {
        public IEnumerable<UserDto> GetUsers(Request request, out int count)
        {
            using var context = new GeneralContext();
            var contexts = context.Set<User>()
                .Include("UserRoles.Role")
                .Where(x => x.Deleted != true)
                .AsQueryable();


            if (request != null)
            {
                if (request.CreateBy > 0)
                    contexts = contexts.Where(x => x.CreateBy == request.CreateBy);

                if (request.Id > 0)
                    contexts = contexts.Where(x => x.Id == request.Id);

                if (request.CompanyId > 0)
                    contexts = contexts.Where(x => x.CompanyId == request.CompanyId);

                if (!string.IsNullOrEmpty(request.Search))
                    contexts = contexts.Where(x => x.Name.Contains(request.Search) ||
                                                   x.Surname.Contains(request.Search) ||
                                                   x.Email.Contains(request.Search) ||
                                                   x.Phone.Contains(request.Search));

                count = contexts.Count();

                if (!string.IsNullOrEmpty(request.Sort))
                    contexts = contexts.OrderBy(request.Sort + (request.Desc ? " desc" : " asc"));

                if (request != null && request.RowCount > 0)
                    contexts = contexts.Skip(request.PageCount * request.RowCount)
                                       .Take(request.RowCount);
            }
            else count = contexts.Count();
            return contexts.Select(x => new UserDto(x)).ToList();
        }

        public User GetByPhone(string phone)
        {
            using var context = new GeneralContext();
            return context.User.FirstOrDefault(x => x.Phone == phone);
        }

        public bool Exists(string phone)
        {
            using var context = new GeneralContext();
            return context.User.Any(x => x.Phone == phone);
        }
    }
}
