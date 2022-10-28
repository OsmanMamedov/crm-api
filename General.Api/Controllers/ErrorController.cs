using General.Core.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace General.Api
{
    public class ErrorController : Controller
    {
       
        [HttpGet("/Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            ResponseService response = new ResponseService();
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            response.Success = false;
            response.Message = exceptionDetails.Error.Message;
            //TODO: Exception Detail
            return Ok(response);
        }


    }
}
