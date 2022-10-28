using General.Core.Entities;
using General.Core.Entities.Concrete;
using General.Entities.Complex.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Business.Abstract
{
    public interface ICompanyService
    {
        ResponseService GetList(Request request);
        ResponseService Add(CompanyDto item);
        ResponseService Update(CompanyDto item);
        ResponseService Delete(CompanyDto item);
    }
}
