using General.Core.DataAccess;
using General.Core.Entities;
using General.Entities.Complex.Dtos;
using General.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.DataAccess.Abstract
{
    public interface ICompanyDal : IEntityRepository<Company>
    {
        IEnumerable<CompanyDto> GetCompanys(Request request, out int count);
        CompanyDto GetCompany(Request request);
    }
}
