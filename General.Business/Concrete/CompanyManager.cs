using General.Business.Abstract;
using General.Core.Entities;
using General.Core.Entities.Concrete;
using General.DataAccess.Abstract;
using General.Entities.Complex.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal _dal;
        public CompanyManager(ICompanyDal CompanyDal)
        {
            _dal = CompanyDal;
        }

        public ResponseService Add(CompanyDto item)
        {
            ResponseService response = new ResponseService
            {
                Data = _dal.Add(item.GetCompany()),
                Success = true
            };

            return response;
        }

        public ResponseService Delete(CompanyDto item)
        {
            ResponseService response = new ResponseService
            {
                Data = _dal.Delete(item.GetCompany()),
                Success = true
            };

            return response;
        }

        public ResponseService GetList(Request request)
        {
            var list = _dal.GetCompanys(request, out int count);
            ResponseService response = new ResponseService
            {
                Data = list,
                Count = count,
                Success = true
            };

            return response;
        }

        public ResponseService Update(CompanyDto item)
        {
            ResponseService response = new ResponseService
            {
                Data = _dal.Update(item.GetCompany()),
                Success = true
            };

            return response;
        }
    }
}
