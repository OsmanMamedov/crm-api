using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Business.Abstract;
using General.Core.Entities;
using General.Entities.Complex.Dtos;
using General.Entities.Complex.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.Api.Controllers.Company
{
    //[Authorize]
    [Route("api/[controller]s")]
    [Produces("application/json")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _service;
        public CompanyController(ICompanyService CompanyService)
        {
            _service = CompanyService;
        }
        public IActionResult Index([FromBody] RequestService<CompanyDto> requestService)
        {  
            //if (GetCurrentUserId() == 0)
            //    return Ok(CouldntUser());
            //var userId = GetCurrentUserId();

            switch (requestService.RequestType)
            {
                case Enums.RequestTypes.Create:
                    requestService.Data.CreateBy = 1;
                    return Ok(_service.Add(requestService.Data));
                case Enums.RequestTypes.Update:
                    requestService.Data.ModifyBy = 1;
                    return Ok(_service.Update(requestService.Data));
                case Enums.RequestTypes.Delete:
                    requestService.Data.ModifyBy = 1;
                    return Ok(_service.Delete(requestService.Data));
                case Enums.RequestTypes.List:
                    return Ok(_service.GetList(requestService.Request));
                default:
                    return Ok(CheckRequestTypeResponse());
            }
        }
    }
}
