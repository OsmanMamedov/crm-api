using General.Business.Abstract;
using General.Core.Concerns.Caching.Microsoft;
using General.Core.Entities;
using General.Core.Entities.Concrete;
using General.DataAccess.Abstract;
using General.Entities;
using General.Entities.Complex.Dtos;
using System;
using System.Collections.Generic;

namespace General.Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleDal _RoleDal;

        public RoleManager()
        {

        }
        public RoleManager(IRoleDal RoleDal)
        {
            _RoleDal = RoleDal;
        }

        public ResponseService Add(RoleDto item)
        {
            ResponseService ResponseService = new ResponseService
            {
                Success = true,
                Data = _RoleDal.Add(item.GetRole())
            };
            return ResponseService;
        }

        public ResponseService Any(RoleDto item)
        {
            ResponseService ResponseService = new ResponseService
            {
                Success = _RoleDal.Any(item.GetRole())
            };
            return ResponseService;
        }

        public ResponseService Delete(RoleDto item)
        {

            ResponseService ResponseService = new ResponseService
            {
                Success = _RoleDal.Delete(item.GetRole())
            };
            return ResponseService;
        }

        public ResponseService Get(Request request)
        {
            ResponseService ResponseService = new ResponseService
            {
                Data = _RoleDal.Get(request),
                Success = true
            };
            return ResponseService;
        }

        public ResponseService GetList(Request request)
        {

            ResponseService responseService = new ResponseService
            {
                Data = _RoleDal.GetList(request),
                Success = true
            };
            return responseService;
        }

        public ResponseService Update(RoleDto item)
        {
            ResponseService ResponseService = new ResponseService
            {
                Data = _RoleDal.Update(item.GetRole()),
                Success = true
            };
            return ResponseService;
        }
    }
}
