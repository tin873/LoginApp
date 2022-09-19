using Microsoft.AspNetCore.Mvc;
using Core.Model;
using System.Net;

namespace LoginApp.Controllers
{
    public abstract class SSControllerBase : ControllerBase
    {
        protected virtual ObjectResult Success(HttpStatusCode code = HttpStatusCode.OK, string message = "Success")
        {
            return StatusCode((int)code, ApiResult.Ok(code, message));
        }
        protected virtual ObjectResult Success<TData>(TData data, HttpStatusCode code = HttpStatusCode.OK, string message = "Success")
        {
            return StatusCode((int)code, ApiResult<TData>.Ok(data, code, message));
        }
    }
}
