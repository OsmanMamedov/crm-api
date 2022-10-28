using General.Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;

namespace General.Api
{
    public abstract class BaseController : Controller
    {
        public ResponseService _response;

        protected BaseController()
        {

        }

        [NonAction]
        public long GetCurrentUserId()
        {
            return Convert.ToInt64(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        [NonAction]
        public long GetCurrentCompanyId()
        {
            var id = Convert.ToInt64(User?.Claims.FirstOrDefault(x => x.Type == "CompanyId").Value);
            return id;
        }
        [NonAction]
        public ResponseService CouldntUser()
        {
            _response = new ResponseService
            {
                Message = "UserNotFound",
                Success = false
            };
            return _response;
        }
        [NonAction]
        public ResponseService CheckRequestTypeResponse()
        {
            _response = new ResponseService
            {
                Message = "PleaseEntryRequestType",
                Success = false
            };
            return _response;
        }
        [NonAction]
        public ResponseService CheckBlankFieldResponse()
        {
            _response = new ResponseService
            {
                Success = false,
                Message = "PleaseFillInTheBlankFields"
            };
            return _response;
        }
        [NonAction]
        public ResponseService CheckPhoneResponse()
        {
            _response = new ResponseService
            {
                Success = false,
                Message = "AlreadyInUse"
            };
            return _response;
        }
    }
}
