using BlogApp.Common;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.V1.Controllers
{
    public class BaseController : ControllerBase
    {
        protected new IActionResult Ok(object result = null)
        {
            return base.Ok(Envelope.Ok(result));
        }

        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(Envelope.Error(errorMessage));
        }
    }
}