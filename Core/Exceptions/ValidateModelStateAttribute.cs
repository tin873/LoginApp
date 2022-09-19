using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Core.Model;

namespace Core.Exceptions
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var hosting = (IWebHostEnvironment)context.HttpContext.RequestServices.
                    GetService(typeof(IWebHostEnvironment));

                var message = hosting.EnvironmentName.ToLower().Contains("development") ?
                        Newtonsoft.Json.JsonConvert.SerializeObject(context.ModelState) : "Dữ liệu không hợp lệ";

                var responseObj = ApiResult.Error(message);

                context.Result = new JsonResult(responseObj)
                {
                    ContentType = "application/json",
                    StatusCode = 400
                };
            }
            else
                base.OnResultExecuting(context);
        }
    }
}
