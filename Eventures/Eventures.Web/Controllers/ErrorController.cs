namespace Eventures.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.Exception = exception.Path;
            ViewBag.ExceptionMessage = exception.Error.Message;
            ViewBag.StackTrace = exception.Error.StackTrace;
            return this.View("/Home/Error");
        }
    }
}