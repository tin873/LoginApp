using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Core.Model;
using System.Collections;
using System.Net;

namespace Core.Exceptions
{
    public class HttpResponseExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<HttpResponseExceptionFilterAttribute> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HttpResponseExceptionFilterAttribute(ILogger<HttpResponseExceptionFilterAttribute> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment=hostEnvironment;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            if (context.Exception is UserFriendlyException userFriendlyException)
            {
                context.Result = new ObjectResult(ApiResult<IDictionary>.Error(
                        context.Exception.Message
                    )
                );
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)userFriendlyException.Status;
                context.ExceptionHandled = true;
            }
            else if (context.Exception != null)
            {
                if (_hostEnvironment.EnvironmentName.EndsWith("Development"))
                    context.Result = new ObjectResult(ApiResult<IDictionary>.Error(context.Exception.Message, context.Exception.Data, HttpStatusCode.InternalServerError));
                else
                    context.Result = new ObjectResult(ApiResult.Error("Lỗi không xác định. Vui lòng liên hệ admin!", HttpStatusCode.InternalServerError));

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
            }
        }
    }
}
