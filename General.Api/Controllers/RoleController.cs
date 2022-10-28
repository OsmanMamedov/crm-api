using General.Business.Abstract;
using General.Core.Entities;
using General.Entities.Complex.Dtos;
using General.Entities.Complex.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.Api
{
    [Route("api/[controller]s")]
    [Produces("application/json")]
    [Authorize]
    public class RoleController : BaseController
    {

        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Index([FromBody] RequestService<RoleDto> requestService)
        {
            if (GetCurrentUserId() == 0)
                return Ok(CouldntUser());

            var userId = GetCurrentUserId();
            switch (requestService.RequestType)
            {
                case Enums.RequestTypes.Create:
                    requestService.Data.CreateBy = userId;
                    return Ok(_service.Add(requestService.Data));
                case Enums.RequestTypes.Read:
                    return Ok(_service.Get(requestService.Request));
                case Enums.RequestTypes.Update:
                    requestService.Data.ModifyBy = userId;
                    return Ok(_service.Update(requestService.Data));
                case Enums.RequestTypes.Delete:
                    requestService.Data.ModifyBy = userId;
                    return Ok(_service.Update(requestService.Data));
                case Enums.RequestTypes.List:
                    return Ok(_service.GetList(requestService.Request));
                default:
                    return Ok(CheckRequestTypeResponse());
            }
        }
    }
}
