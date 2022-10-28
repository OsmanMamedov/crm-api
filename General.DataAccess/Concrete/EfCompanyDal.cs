using General.Core.DataAccess.EntityFramework;
using General.Core.Entities;
using General.DataAccess.Abstract;
using General.Entities.Complex.Dtos;
using General.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace General.DataAccess.Concrete
{
    public class EfCompanyDal : EfEntityRepository<Company, GeneralContext>, ICompanyDal
    {
        public CompanyDto GetCompany(Request request)
        {
            using var context = new GeneralContext();
            var contexts = new CompanyDto(context.Set<Company>()
                .FirstOrDefault(x => x.Id == request.Id));

            return contexts;
        }

        public IEnumerable<CompanyDto> GetCompanys(Request request, out int count)
        {
            using var context = new GeneralContext();
            var contexts = context.Set<Company>()
                .Where(x => x.Deleted != true)
                .AsQueryable();

            if (request != null)
            {
                if (request.Id > 0)
                    contexts = contexts.Where(x => x.Id == request.Id);

                if (!string.IsNullOrEmpty(request.Search))
                    contexts = contexts.Where(x => x.Name.Contains(request.Search));

                count = contexts.Count();

                if (!string.IsNullOrEmpty(request.Sort))
                    contexts = contexts.OrderBy(request.Sort + (request.Desc ? " desc" : " asc"));

                if (request != null && request.RowCount > 0)
                    contexts = contexts.Skip(request.PageCount * request.RowCount)
                                       .Take(request.RowCount);
            }
            else
                count = contexts.Count();
            
            return contexts.Select(x => new CompanyDto(x)).ToList();
        }
    }
}
